using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;

namespace TheStore.Web.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NavigationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {

            return PartialView(_context.Categories.Include(x => x.Categories).Include(x => x.Products).ToList());
        }

        private List<Brand> GetBrands(Category category)
        {
            var result = new List<Brand>();

            if (category.Products.Any())
            {
                var brands = (from product in category.Products select product.Brand).Distinct().ToList();
                result.AddRange(brands);
            }

            return result;
        }
    }
}