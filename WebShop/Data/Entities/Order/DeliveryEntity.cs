using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Basket;
using WebShop.Data.Entities.Order;

namespace WebShop.Data.Entities.Product
{
    [Table("tblDelivery")]
    public class DeliveryEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<PostOfficeEntity> Offices { get; set; }

    }
}

