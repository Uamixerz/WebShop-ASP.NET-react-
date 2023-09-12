using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebShop.Data.Entities.Earth;
using WebShop.Data.Entities.Product;

namespace WebShop.Data.Entities.Characteristics
{
    
    [Table("tblCharacteristicsProduct")]
    public class CharacteristicsProductEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Characteristic")]
        public int CharacteristicId { get; set; }
        public virtual CharacteristicsEntity Characteristic { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
