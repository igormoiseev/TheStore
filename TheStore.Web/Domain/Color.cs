using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace TheStore.Web.Domain
{
    [Table("Colors")]
    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        public string Name { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}