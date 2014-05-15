using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models;
using TheStore.Web.Models.Category;

namespace TheStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public ActionResult Index(string categoryUrl)
        {
            var model = _context.Categories.Project<Category>().To<CategoryViewModel>().SingleOrDefault(i => i.CategoryUrl == categoryUrl);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Категория c Url ({0}) не найдена. Возможно она была удалена.", categoryUrl));
            }

            return View(model);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult CategoryWidget()
        {
            var model = new CategoryWidgetViewModel(){Categories = _context.Categories.ToList()};
            return PartialView(model);
        }

        public ActionResult Manage()
        {
            return View(_context.Categories.Include(x => x.ParentCategory).OrderBy(x => x.SequenceNumber).ToList());
        }

        public ActionResult New()
        {
            var model = new NewCategoryForm();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add new category")]
        public ActionResult New(NewCategoryForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Categories.Add(new Category
            {
                Name = form.Name,
                ParentCategoryId = form.ParentCategoryId == 0 ? (int?)null : form.ParentCategoryId,
                SequenceNumber = form.SequenceNumber,
                Description = form.Description,
                CategoryUrl = form.CategoryUrl
            });

            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Добавлена категория \"{0}\"", form.Name));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Categories.Project<Category>().To<EditCategoryForm>().SingleOrDefault(i => i.CategoryId == id);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Категория c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update category {id}")]
        public ActionResult Edit(EditCategoryForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var category = _context.Categories.SingleOrDefault(x => x.CategoryId == form.CategoryId);
            if (category == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Категория c ID ({0}) не найдена. Возможно она была удалена.", form.CategoryId));
            }

            category.Name = form.Name;
            category.ParentCategoryId = form.ParentCategoryId == 0 ? null : form.ParentCategoryId;
            category.SequenceNumber = form.SequenceNumber;
            category.CategoryUrl = form.CategoryUrl;
            category.Description = form.Description;

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Категория \"{0}\" обновлена.", form.Name));
        }

        [Log("Delete category {id}")]
        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Категория c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            var name = category.Name;
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Категория \"{0}\" удалена.", name));
        }
    }
}