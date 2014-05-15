using System.ComponentModel.DataAnnotations;

namespace TheStore.Web.Models.Category
{
    public class NewCategoryForm
    {
        [Display(Name = "Название категории"), Required]
        public string Name { get; set; }

        [Display(Name = "URL"), Required]
        public string CategoryUrl { get; set; }

        [Required, Display(Name = "Принадлежит категории"), DataType("ParentCategoryId")]
        public int ParentCategoryId { get; set; }

        [Display(Name = "Порядковый номер")]
        public int SequenceNumber { get; set; }

        [Display(Name = "Описание"), Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}