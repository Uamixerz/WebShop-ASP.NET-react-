using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebShop.Data.Entities.Product;
using WebShop.Data.Entities.Basket;

namespace WebShop.Data.Entities.Order
{
    [Table("tblOrderStatus")]
    public class OrderStatusEntity
    {
            [Key]
            public int Id { get; set; }

            [Required, StringLength(255)]
            public string Name { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
