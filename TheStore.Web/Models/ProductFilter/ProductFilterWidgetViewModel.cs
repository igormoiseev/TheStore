using System.Collections.Generic;

namespace TheStore.Web.Models.ProductFilter
{
    public class ProductFilterWidgetViewModel
    {
        public Domain.Category Category { get; set; }
        public List<Domain.Category> Categories { get; set; }
        public List<Domain.Brand> Brands { get; set; }
    }
}