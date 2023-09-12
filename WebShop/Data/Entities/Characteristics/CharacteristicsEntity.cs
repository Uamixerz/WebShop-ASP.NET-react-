using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebShop.Data.Entities.Earth;
using WebShop.Data.Entities.Order;

namespace WebShop.Data.Entities.Characteristics
{
    [Table("tblCharacteristics")]
    public class CharacteristicsEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<CharacteristicsCategoryEntity> CharacteristicsCategory { get; set; }

        public virtual ICollection<CharacteristicsProductEntity> CharacteristicsProduct { get; set; }
    }
}
