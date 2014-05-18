using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models.Product;

namespace TheStore.Web.Controllers
{
    public class ProductController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(string categoryUrl, string brandUrl, string productUrl)
        {
            var model = _context.Products.SingleOrDefault(x => x.Url == productUrl);

            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Товар c URL ({0}) не найден. Возможно он был удален.", productUrl));
            }

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult ProductListWidget(ProductFilter productFilter, string categoryUrl, string brandUrl)
        {
            var category = _context.Categories.Include(x => x.Categories).SingleOrDefault(x => x.CategoryUrl == categoryUrl);
            var brand = _context.Brands.SingleOrDefault(x => x.BrandUrl == brandUrl);
            var availableProducts = new List<Product>();

            if (category == null)
            {
                return
                    this.RedirectToAction<HomeController>(x => x.Index())
                        .WithError(string.Format(
                            "Категория товаров c URL ({0}) не найдена. Возможно она была удалена.", categoryUrl));
            }

            if (category.Products.Any())
                availableProducts.AddRange(category.Products);

            if (category.Categories.Any())
            {
                foreach (var subCategory in category.Categories)
                {
                    if (subCategory.Products.Any())
                        availableProducts.AddRange(subCategory.Products);
                }
            }

            if (brand != null)
            {
                availableProducts = availableProducts.Where(x => x.Brand.BrandId == brand.BrandId).ToList();
            }

            var model = productFilter.Filter(availableProducts);

            return PartialView(model);
        }

        public ActionResult Manage()
        {
            return View(_context.Products.ToList());
        }

        public ActionResult New()
        {
            var form = new NewProductForm();
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log("Add new Product")]
        public ActionResult New(NewProductForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var options = new List<Option>();
            if (form.Options != null)
            {
                foreach (var option in form.Options)
                {
                    var tempOption = _context.Options.SingleOrDefault(x => x.OptionId == option.OptionId);
                    if (tempOption != null)
                    {
                        options.Add(tempOption);
                    }
                }
            }

            _context.Products.Add(new Product
            {
                Name = form.Name,
                Code = form.Code,
                InitialPrice = form.InitialPrice,
                ProductFee = form.ProductFee,
                DeliveryCharge = form.DeliveryCharge,
                BrandId = form.BrandId,
                CreatedAt = DateTime.UtcNow,
                Description = form.Description,
                Url = form.Url,
                CategoryId = form.CategoryId,
                Options = options
            });

            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Добавлен новый товар \"{0}\"", form.Name));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Products.Project<Product>().To<EditProductForm>().SingleOrDefault(i => i.ProductId == id);
            if (model == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Товар c ID ({0}) не найден. Возможно он был удален.", id));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update Product {id}")]
        public ActionResult Edit(EditProductForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var options = new List<Option>();
            if (form.Options != null)
            {
                foreach (var option in form.Options)
                {
                    var tempOption = _context.Options.SingleOrDefault(x => x.OptionId == option.OptionId);
                    if (tempOption != null)
                    {
                        options.Add(tempOption);
                    }
                }
            }

            var product = _context.Products.SingleOrDefault(x => x.ProductId == form.ProductId);
            if (product == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Товар c ID ({0}) не найден. Возможно он был удален.", form.ProductId));
            }

            product.Name = form.Name;
            product.Code = form.Code;
            product.Url = form.Url;
            product.InitialPrice = form.InitialPrice;
            product.ProductFee = form.ProductFee;
            product.DeliveryCharge = form.DeliveryCharge;
            product.Description = form.Description;
            product.BrandId = form.BrandId;
            product.CategoryId = form.CategoryId;

            //var optionsToRemove = _context.Options.Where(x => x.Products.Any(p=> p.ProductId == form.ProductId));
            //_context.Options.RemoveRange(optionsToRemove);

            //product.Options = options;

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Товар \"{0}\" обновлен.", form.Name));
        }

        [Log("Delete Product {id}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return
                    this.RedirectToAction(x => x.Manage())
                        .WithError(string.Format("Товар c ID ({0}) не найден. Возможно он был удален.", id));
            }

            var name = product.Name;
            _context.Products.Remove(product);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.Manage()).WithSuccess(string.Format("Товар \"{0}\" удален.", name));
        }
	}
}