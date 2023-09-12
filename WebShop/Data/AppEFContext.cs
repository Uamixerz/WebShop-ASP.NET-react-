using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Identity;
using WebShop.Data.Entities.Product;
using System.Reflection.Emit;
using WebShop.Data.Entities.Basket;
using WebShop.Data.Entities.Earth;
using WebShop.Data.Entities.Order;
using WebShop.Data.Entities.Characteristics;

namespace WebShop.Data
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        {

        }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductImagesEntity> ProductImages { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<BasketEntity> Baskets { get; set; }

        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<RegionEntity> Regiones { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<DeliveryEntity> Deliveries { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderStatusEntity> OrderStatus { get; set; }
        public DbSet<PayStatusEntity> PayStatus { get; set; }
        public DbSet<PayMethodEntity> PayMethod { get; set; }

        public DbSet<PostOfficeEntity> PostOffices { get; set; }

        public DbSet<CharacteristicsEntity> Characteristics { get; set; }
        public DbSet<CharacteristicsProductEntity> CharacteristicsProduct { get; set; }
        public DbSet<CharacteristicsCategoryEntity> CharacteristicsCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });
            builder.Entity<ProductEntity>()
            .HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);

            builder.Entity<BasketEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.ProductId });
            });
        }
    }
}
