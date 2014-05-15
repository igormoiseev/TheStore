using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;

namespace TheStore.Web.Filters
{
    public class CategorySelectListPopulatorAttribute : ActionFilterAttribute
    {
        public ApplicationDbContext Context { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult != null && viewResult.Model is IHaveCategorySelectList)
            {
                ((IHaveCategorySelectList) viewResult.Model).AvailableCategories = GetAvailableCategories();
            }
        }

        private IEnumerable<SelectListItem> GetAvailableCategories()
        {
            return Context.Categories.ToList().Select(category => new SelectListItem { Text = category.Name, Value = category.CategoryId.ToString() }).ToList();
        }
    }

    public interface IHaveCategorySelectList
    {
        IEnumerable<SelectListItem> AvailableCategories { get; set; }
    }
}