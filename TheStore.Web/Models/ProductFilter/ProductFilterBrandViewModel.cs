using System.Collections.Generic;

namespace TheStore.Web.Models.ProductFilter
{
    public class ProductFilterBrandViewModel
    {
        public List<Domain.Brand> Brands { get; set; }
        public Domain.ProductFilter Filter { get; set; }
    }
}