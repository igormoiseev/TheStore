using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheStore.Web.Models.Option
{
    public class EditOptionForm
    {
        [HiddenInput]
        public int OptionId { get; set; }
        [Required, Display(Name = "Название")]
        public string Name { get; set; }
        [Required, Display(Name = "URL")]
        public string Url { get; set; }
        [Required, Display(Name = "Описание"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required, Display(Name = "Характеристика"), DataType("CharacteristicId")]
        public int CharacteristicId { get; set; }
    }
}