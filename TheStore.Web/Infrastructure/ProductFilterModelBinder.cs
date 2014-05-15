using System;
using System.Web.Mvc;
using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public class ProductFilterModelBinder : IModelBinder
    {
        private const string FilterKey = "_Filter";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
            {
                throw new InvalidOperationException("Не удалось обновить экземпляры.");
            }
            var filter = (ProductFilter)controllerContext.HttpContext.Session[FilterKey];
            if (filter == null)
            {
                filter = new ProductFilter();
                controllerContext.HttpContext.Session[FilterKey] = filter;
            }
            return filter;
        }
    }
}