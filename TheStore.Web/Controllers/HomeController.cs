using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Infrastructure;
using TheStore.Web.Models.Home;

namespace TheStore.Web.Controllers
{
    public class HomeController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                LatestProducts = _context.Products.Include(x => x.Photos).OrderByDescending(x => x.CreatedAt).Take(12).ToList()
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ShippingAndPayment()
        {
            return View();
        }

        public ActionResult Warranty()
        {
            return View();
        }
    }
}