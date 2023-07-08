using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebShop.Data.Entities.Basket;
using WebShop.Data.Entities.Order;

namespace WebShop.Data.Entities.Identity
{
    public class UserEntity : IdentityUser<int>
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }

        public virtual ICollection<BasketEntity> Baskets { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
