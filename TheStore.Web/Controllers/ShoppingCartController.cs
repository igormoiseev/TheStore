using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Models.ShoppingCart;

namespace TheStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult ShoppingCartWidget(ShoppingCart shoppingCart)
        {
            return PartialView(shoppingCart);
        }

        public ActionResult ShowCart(ShoppingCart shoppingCart, string returnUrl)
        {
            var model = new ShoppingCartViewModel {ShoppingCart = shoppingCart, ReturnUrl = returnUrl};
            return View(model);
        }

        [HttpPost]
        public ActionResult AddToCart(ShoppingCart shoppingCart, int productId, string returnUrl)
        {
            var product = _context.Products.Include(x => x.Photos).Include(x => x.Category).Include(x => x.Brand).SingleOrDefault(x => x.ProductId == productId);
            shoppingCart.AddItem(product, 1);
            return RedirectToAction("ShowCart", new{ returnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult RemoveItem(ShoppingCart shoppingCart, int productId, string returnUrl)
        {
            if (shoppingCart.CartItems.Any(x => x.Product.ProductId == productId) && shoppingCart.CartItems.Where(x => x.Product.ProductId == productId).SingleOrDefault().Quantity > 1)
            {
                var product = _context.Products.Include(x => x.Photos).SingleOrDefault(x => x.ProductId == productId);
                shoppingCart.AddItem(product, -1);
            }

            return RedirectToAction("ShowCart", new { returnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult RemoveFromCart(ShoppingCart shoppingCart, int productId, string returnUrl)
        {
            var product = _context.Products.Include(x => x.Photos).SingleOrDefault(x => x.ProductId == productId);
            shoppingCart.RemoveItem(product);

            return RedirectToAction("ShowCart", new { returnUrl = returnUrl });
        }
    }
}