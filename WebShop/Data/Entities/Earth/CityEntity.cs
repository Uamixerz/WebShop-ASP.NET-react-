using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entities.Order;

namespace WebShop.Data.Entities.Earth
{
    [Table("tblCities")]
    public class CityEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public virtual RegionEntity Region { get; set; }

        public virtual ICollection<PostOfficeEntity> PostOffices { get; set; }
    }
}
