using System.Collections.Generic;
using System.Web;

namespace TheStore.Web.Models.Photo
{
    public class NewPhotoForm
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsForFrontend { get; set; }
        public bool? IsForGallery { get; set; }
        public bool? IsForShoppingCart { get; set; }
        public List<Domain.Photo> Photos { get; set; }
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}