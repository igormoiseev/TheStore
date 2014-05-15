using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models.Characteristic;

namespace TheStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CharacteristicController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public CharacteristicController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Manage()
        {
            return View(_context.Characteristics.ToList());
        }

        public ActionResult New()
        {
            var form = new NewCharacteristicForm();
            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add new characteristic")]
        public ActionResult New(NewCharacteristicForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Characteristics.Add(new Characteristic
            {
                Name = form.Name,
                Description = form.Description,
                Url = form.Url,
                SequenceNumber = form.SequenceNumber,
                IsFilterable = form.IsFilterable
            });

            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Добавлена новая характеристика \"{0}\"", form.Name));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Characteristics.Project<Characteristic>().To<EditCharacteristicForm>().SingleOrDefault(i => i.CharacteristicId == id);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Характеристика c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update characteristic {id}")]
        public ActionResult Edit(EditCharacteristicForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var characteristic = _context.Characteristics.SingleOrDefault(x => x.CharacteristicId == form.CharacteristicId);
            if (characteristic == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Характеристика c ID ({0}) не найдена. Возможно она была удалена.", form.CharacteristicId));
            }

            characteristic.Name = form.Name;
            characteristic.Url = form.Url;
            characteristic.Description = form.Description;
            characteristic.SequenceNumber = form.SequenceNumber;
            characteristic.IsFilterable = form.IsFilterable;

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Характеристика \"{0}\" обновлена.", form.Name));
        }

        [Log("Delete characteristic {id}")]
        public ActionResult Delete(int id)
        {
            var characteristic = _context.Characteristics.Find(id);
            if (characteristic == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Характеристика c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            var name = characteristic.Name;
            _context.Characteristics.Remove(characteristic);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Характеристика \"{0}\" удалена.", name));
        }
    }
}