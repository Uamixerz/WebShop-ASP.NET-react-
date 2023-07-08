using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Identity;
using WebShop.Data.Entities.Product;

namespace WebShop.Data.Entities.Order
{
    public class OrderItemEntity
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Кількість товару
        /// </summary>
        public int Quintity { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual OrderEntity Order { get; set; }

    }
}
