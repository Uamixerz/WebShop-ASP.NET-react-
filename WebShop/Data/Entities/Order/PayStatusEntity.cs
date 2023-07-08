using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Data.Entities.Order
{
    [Table("tblPayStatus")]
    public class PayStatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
