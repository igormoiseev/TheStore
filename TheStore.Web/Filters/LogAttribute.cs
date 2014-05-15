using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Infrastructure;

namespace TheStore.Web.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private IDictionary<string, object> _parameters;
        public ApplicationDbContext Context { get; set; }
        public ICurrentUser CurrentUser { get; set; }
        public string Description { get; set; }
        public LogAttribute(string description)
        {
            Description = description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _parameters = filterContext.ActionParameters;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var description = _parameters.Aggregate(Description, (current, kvp) => current.Replace("{" + kvp.Key + "}", kvp.Value.ToString()));

            Context.ActionLogs.Add(new ActionLog(CurrentUser.User, filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, description));

            Context.SaveChanges();
        }
    }
}