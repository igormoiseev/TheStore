using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Infrastructure;
using TheStore.Web.Models.ProductFilter;

namespace TheStore.Web.Controllers
{
    public class ProductFilterController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public ProductFilterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult ProductFilterWidget(string categoryUrl)
        {
            var category = _context.Categories.Include(x => x.Categories).Include(x => x.Products).SingleOrDefault(x => x.CategoryUrl == categoryUrl);
            
            var model = new ProductFilterWidgetViewModel { Category = category, Categories = GetAvailableCategories(category), Brands = GetAvailableBrands(category) };
            return PartialView(model);
        }

        private List<Category> GetAvailableCategories(Category category)
        {
            if (category.Categories.Any())
                return category.Categories.ToList();
            else
            {
                var parentCategory =
                    _context.Categories.Include(x => x.Categories)
                        .FirstOrDefault(x => x.CategoryId == category.ParentCategoryId);
                return parentCategory.Categories.ToList();
            }
        }

        private List<Brand> GetAvailableBrands(Category category)
        {
            var result = new List<Brand>();

            var availableProducts = new List<Product>();
            if(category.Products.Any())
                availableProducts.AddRange(category.Products);

            if (category.Categories.Any())
            {
                foreach (var subCategory in category.Categories)
                {
                    if(subCategory.Products.Any())
                        availableProducts.AddRange(subCategory.Products);
                }
            }

            result = (from availableProduct in availableProducts select availableProduct.Brand).Distinct().ToList();

            return result;
        }

        public PartialViewResult BrandFilterWidget(ProductFilter productFilter, string categoryUrl)
        {
            var model = new ProductFilterBrandViewModel();
            return PartialView(model);
        }

        public PartialViewResult ProductOptionFilterWidget(ProductFilter productFilter, string categoryUrl)
        {
            var model = new ProductOptionFilterViewModel
            {
                //Characteristics = _context.Characteristics.Where(x => x.Category.CategoryUrl == categoryUrl).ToList(),
                //Filter = productFilter
            };
            return PartialView(model);
        }

        public ActionResult AddBrand(ProductFilter filter, int brandId)
        {
            return RedirectToAction("Index", "Category");
        }

        public ActionResult AddOption(ProductFilter filter, int optionId)
        {
            //var option = _context.Options.SingleOrDefault(x => x.OptionId == optionId);
            //if (option == null)
            //{
            //    return
            //        this.RedirectToAction<HomeController>(x => x.Index())
            //            .WithError("Выбранная опиця не найдена. Возможно она была удалена.");
            //}

            //if (filter.Options.Any(x => x.OptionId == optionId))
            //{
            //    filter.RemoveOption(option);
            //    return this.RedirectToAction<CategoryController>(x => x.Index(option.Characteristic.Category.CategoryUrl));
            //}
            //filter.AddOption(option);

            //return this.RedirectToAction<CategoryController>(x => x.Index(option.Characteristic.Category.CategoryUrl));
            return RedirectToAction("Index", "Category");
        }
    }
}