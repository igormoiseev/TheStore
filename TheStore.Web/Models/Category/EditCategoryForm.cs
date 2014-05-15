using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheStore.Web.Models.Category
{
    public class EditCategoryForm
    {
        [HiddenInput]
        public int CategoryId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "Принадлежит категории"), DataType("ParentCategoryId")]
        public int? ParentCategoryId { get; set; }

        [Display(Name = "Порядковый номер")]
        public int SequenceNumber { get; set; }

        [Display(Name = "URL")]
        public string CategoryUrl { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}