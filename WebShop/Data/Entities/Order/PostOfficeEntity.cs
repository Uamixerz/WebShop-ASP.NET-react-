using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Earth;
using WebShop.Data.Entities.Product;

namespace WebShop.Data.Entities.Order
{
    [Table("tblPostOffices")]
    public class PostOfficeEntity
    {
        public int Id { get; set; }
        public string PostIndex { get; set; }

        public string Street { get; set; }

        public string Name { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual CityEntity City { get; set; }

        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        public virtual DeliveryEntity Delivery { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
