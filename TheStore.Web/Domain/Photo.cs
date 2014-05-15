using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public bool? IsForFrontend { get; set; }
        public bool? IsForGallery { get; set; }
        public bool? IsForShoppingCart { get; set; }
        public virtual Product Product { get; set; }
    }
}