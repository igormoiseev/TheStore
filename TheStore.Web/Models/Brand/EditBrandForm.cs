using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheStore.Web.Models.Brand
{
    public class EditBrandForm
    {
        [HiddenInput]
        public int BrandId { get; set; }

        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "URL")]
        public string BrandUrl { get; set; }

        [Required, Display(Name = "Описание"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}