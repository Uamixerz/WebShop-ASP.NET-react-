using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Basket;
using WebShop.Data.Entities.Identity;
using WebShop.Data.Entities.Product;

namespace WebShop.Data.Entities.Order
{
    [Table("tblOrders")]
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        public virtual ICollection<OrderItemEntity> Items { get; set; }

        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }
        public virtual OrderStatusEntity OrderStatus { get; set; }

        [ForeignKey("PayMethod")]
        public int PayMethodId { get; set; }
        public virtual PayMethodEntity PayMethod { get; set; }

        [ForeignKey("PayStatus")]
        public int? PayStatusId { get; set; }
        public virtual PayStatusEntity PayStatus { get; set; }

        [ForeignKey("PostOffice")]
        public int PostOfficeId { get; set; }
        public virtual PostOfficeEntity PostOffice { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }


    }
}
