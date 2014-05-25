using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models.Color;

namespace TheStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ColorController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public ColorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Manage()
        {
            return View(_context.Colors.ToList());
        }

        public ActionResult New()
        {
            var form = new NewColorForm();
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log("Add new color")]
        public ActionResult New(NewColorForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Colors.Add(new Color
            {
                Name = form.Name
            });

            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Добавлен новый цвет \"{0}\"", form.Name));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Colors.Project<Color>().To<EditColorForm>().SingleOrDefault(i => i.ColorId == id);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Цвет c ID ({0}) не найден. Возможно он был удален.", id));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update characteristic {id}")]
        public ActionResult Edit(EditColorForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var color = _context.Colors.SingleOrDefault(x => x.ColorId == form.ColorId);
            if (color == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Цвет c ID ({0}) не найден. Возможно он был удален.", form.ColorId));
            }

            color.Name = form.Name;

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Цвет \"{0}\" обновлен.", form.Name));
        }

        [Log("Delete color {id}")]
        public ActionResult Delete(int id)
        {
            var color = _context.Colors.Find(id);
            if (color == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Цвет c ID ({0}) не найден. Возможно он был удален.", id));
            }

            var name = color.Name;
            _context.Colors.Remove(color);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Цвет \"{0}\" удален.", name));
        }
    }
}