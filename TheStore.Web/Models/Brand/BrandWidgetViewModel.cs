using System.Collections.Generic;

namespace TheStore.Web.Models.Brand
{
    public class BrandWidgetViewModel
    {
        public Domain.Brand SelectedBrand { get; set; }
        public Domain.Category Category { get; set; }
        public List<Domain.Brand> Brands { get; set; }
    }
}