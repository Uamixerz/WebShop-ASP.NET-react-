using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Order;

namespace WebShop.Data.Entities.Earth
{
    [Table("tblRegiones")]
    public class RegionEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual CountryEntity Country { get; set; }

        public virtual ICollection<CityEntity> Cities { get; set; }
    }
}
