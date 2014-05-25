using System.ComponentModel.DataAnnotations;

namespace TheStore.Web.Models.Color
{
    public class NewColorForm
    {
        [Required]
        [Display(Name = "Цвет")]
        public string Name { get; set; }
    }
}