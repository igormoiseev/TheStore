using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models.Brand;

namespace TheStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BrandController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public ActionResult Index(string brandUrl, string categoryUrl)
        {
            var brand = _context.Brands.FirstOrDefault(x => x.BrandUrl == brandUrl);
            var category = _context.Categories.SingleOrDefault(x => x.CategoryUrl == categoryUrl);

            var model = new BrandViewModel {Brand = brand, Category = category};

            return View(model);
        }

        public ActionResult Manage()
        {
            return View(_context.Brands.ToList());
        }

        public ActionResult New()
        {
            var form = new NewBrandForm();
            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add new brand")]
        public ActionResult New(NewBrandForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Brands.Add(new Brand { Name = form.Name, Description = form.Description, BrandUrl = form.BrandUrl});

            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Добавлен новый производитель \"{0}\"", form.Name));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Brands.Project<Brand>().To<EditBrandForm>().SingleOrDefault(i => i.BrandId == id);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Производитель c ID ({0}) не найден. Возможно он был удален.", id));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update brand {id}")]
        public ActionResult Edit(EditBrandForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var brand = _context.Brands.SingleOrDefault(x => x.BrandId == form.BrandId);
            if (brand == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Производитель c ID ({0}) не найден. Возможно он был удален.", form.BrandId));
            }

            brand.Name = form.Name;
            brand.BrandUrl = form.BrandUrl;
            brand.Description = form.Description;

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Производитель \"{0}\" обновлен.", form.Name));
        }

        [Log("Delete brand {id}")]
        public ActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Производитель c ID ({0}) не найден. Возможно он был удален.", id));
            }

            var name = brand.Name;
            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Производитель \"{0}\" удален.", name));
        }
	}
}