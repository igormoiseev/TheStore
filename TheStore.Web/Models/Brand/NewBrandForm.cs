using System.ComponentModel.DataAnnotations;

namespace TheStore.Web.Models.Brand
{
    public class NewBrandForm
    {
        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "URL")]
        public string BrandUrl { get; set; }

        [Required, Display(Name = "Описание"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}