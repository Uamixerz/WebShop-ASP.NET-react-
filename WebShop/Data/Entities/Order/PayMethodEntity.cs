using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Basket;

namespace WebShop.Data.Entities.Order
{
    [Table("tblPayMethod")]
    public class PayMethodEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
