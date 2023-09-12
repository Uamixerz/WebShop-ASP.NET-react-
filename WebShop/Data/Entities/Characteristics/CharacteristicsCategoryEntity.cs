using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebShop.Data.Entities.Product;

namespace WebShop.Data.Entities.Characteristics
{
    [Table("tblCharacteristicsCategory")]
    public class CharacteristicsCategoryEntity
    {

            [Key]
            public int Id { get; set; }

            [ForeignKey("Characteristic")]
            public int CharacteristicId { get; set; }
            public virtual CharacteristicsEntity Characteristic { get; set; }

            [ForeignKey("Category")]
            public int CategoryId { get; set; }
            public virtual CategoryEntity Category { get; set; }
        
    }
}
