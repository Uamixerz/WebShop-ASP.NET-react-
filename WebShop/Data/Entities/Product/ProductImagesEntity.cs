using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Data.Entities.Product
{
    [Table("tblProductImages")]
    public class ProductImagesEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        [Required, StringLength(255)]
        public string UrlImage { get; set; }

        [ForeignKey("Product")]
        public int ? ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
        
    }
}
