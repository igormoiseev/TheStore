using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;

namespace TheStore.Web.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NavigationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public PartialViewResult Menu()
        {
            return PartialView(_context.Categories.Include(x => x.Categories).ToList());
        }
	}
}