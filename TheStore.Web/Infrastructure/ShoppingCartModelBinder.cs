using System;
using System.Web.Mvc;
using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public class ShoppingCartModelBinder : IModelBinder
    {
        private const string CartKey = "_ShoppingCart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if(bindingContext.Model != null)
                throw new InvalidOperationException("Не удалось обновить экземпляры");
            var cart = (ShoppingCart) controllerContext.HttpContext.Session[CartKey];
            if (cart == null)
            {
                cart = new ShoppingCart();
                controllerContext.HttpContext.Session[CartKey] = cart;
            }
            return cart;
        }
    }
}