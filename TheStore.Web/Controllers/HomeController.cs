using System.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Infrastructure;

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
            return View();
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