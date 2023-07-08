using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Data.Entities.Earth
{
    [Table("tblCountries")]
    public class CountryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<RegionEntity> Regiones { get; set; }
    }
}
