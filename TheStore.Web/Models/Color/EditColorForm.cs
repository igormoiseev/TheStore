using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheStore.Web.Models.Color
{
    public class EditColorForm
    {
        [HiddenInput]
        public int ColorId { get; set; }
        [Required]
        [Display(Name = "Цвет")]
        public string Name { get; set; }
    }
}