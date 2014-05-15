using System.Collections.Generic;

namespace TheStore.Web.Models.Category
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public int SequenceNumber { get; set; }
        public string Description { get; set; }
    }
}