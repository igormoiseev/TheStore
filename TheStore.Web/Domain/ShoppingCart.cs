using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TheStore.Web.Domain
{
    public class ShoppingCart
    {
        private List<CartItem> _cartItems;
        public ReadOnlyCollection<CartItem> CartItems {
            get { return _cartItems.AsReadOnly(); }
        }

        public ShoppingCart()
        {
            _cartItems = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var orderItem = _cartItems.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if (orderItem == null)
            {
                _cartItems.Add(new CartItem{Product = product, Quantity = quantity});
            }
            else
            {
                orderItem.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            _cartItems.RemoveAll(x => x.Product.ProductId == product.ProductId);
        }

        public decimal GetTotalPrice()
        {
            return _cartItems.Sum(x => x.Product.GetProductPrice()*x.Quantity);
        }

        public decimal GetDeliveryPrice()
        {
            if (GetTotalPrice() < 500)
            {
                return 30;
            }
            return 0;
        }

        public int GetTotalQuantity()
        {
            return _cartItems.Sum(x => x.Quantity);
        }

        public void Clear()
        {
            _cartItems.Clear();
        }
    }
}