using TheStore.Web.Models.Order;

namespace TheStore.Web.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public Domain.ShoppingCart ShoppingCart { get; set; }
        public QuickOrderForm QuickOrder { get; set; }
        public string ReturnUrl { get; set; }
    }
}