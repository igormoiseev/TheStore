using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TheStore.Web.Models.Photo
{
    public class EditPhotoForm
    {
        [HiddenInput]
        public int PhotoId { get; set; }

        public string Src { get; set; }
        public string ProductName { get; set; }
        public string Alt { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}