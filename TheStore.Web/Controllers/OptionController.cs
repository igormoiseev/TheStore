using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models.Option;

namespace TheStore.Web.Controllers
{
    public class OptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage()
        {
            return View(_context.Options.ToList());
        }

        public ActionResult New()
        {
            var form = new NewOptionForm();
            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add new Option")]
        public ActionResult New(NewOptionForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Options.Add(new Option { Name = form.Name, Description = form.Description, Url = form.Url, CharacteristicId = form.CharacteristicId });

            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Добавлена новая опция \"{0}\"", form.Name));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Options.Project<Option>().To<EditOptionForm>().SingleOrDefault(i => i.OptionId == id);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Опция c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update Option {id}")]
        public ActionResult Edit(EditOptionForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var option = _context.Options.SingleOrDefault(x => x.OptionId == form.OptionId);
            if (option == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Опция c ID ({0}) не найдена. Возможно она была удалена.", form.OptionId));
            }

            option.Name = form.Name;
            option.Url = form.Url;
            option.Description = form.Description;
            option.CharacteristicId = form.CharacteristicId;

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Опция \"{0}\" обновлена.", form.Name));
        }

        [Log("Delete Option {id}")]
        public ActionResult Delete(int id)
        {
            var option = _context.Options.Find(id);
            if (option == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Опция c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            var name = option.Name;
            _context.Options.Remove(option);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Опция \"{0}\" удалена.", name));
        }
	}
}