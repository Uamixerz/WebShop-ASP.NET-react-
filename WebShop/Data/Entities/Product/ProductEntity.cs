using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Basket;
using WebShop.Data.Entities.Characteristics;
using WebShop.Data.Entities.Order;

namespace WebShop.Data.Entities.Product
{
    [Table("tblProducts")]
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public int Priority { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }

        public bool HomePageSelection { get; set; }
        public int? HomePagePriority { get; set; }

        public virtual ICollection<ProductImagesEntity> Images { get; set; }
        public virtual ICollection<BasketEntity> Baskets { get; set; }
        public virtual ICollection<OrderItemEntity> OrdersItem { get; set; }

        public virtual ICollection<CharacteristicsProductEntity> CharacteristicsProduct { get; set; }
    }
}
