using System.Web.Mvc;
using Antlr.Runtime.Misc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace TheStore.Web.Infrastructure
{
    public class ActionFilterRegistry : Registry
    {
        public ActionFilterRegistry(Func<IContainer> containerFactory)
        {
            For<IFilterProvider>()
                    .Use(new StructureMapFilterProvider(containerFactory));

            Policies.SetAllProperties(x => x.Matching(p =>
                p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
                p.DeclaringType.Namespace.StartsWith("TheStore") &&
                !p.PropertyType.IsPrimitive &&
                p.PropertyType != typeof(string)));
        }
    }
}