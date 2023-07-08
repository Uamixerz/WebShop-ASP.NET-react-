using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebShop.Constants;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Earth;
using WebShop.Data.Entities.Identity;
using WebShop.Data.Entities.Order;
using WebShop.Data.Entities.Product;

namespace WebShop.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<AppEFContext>();
                var userNamager = service.GetRequiredService<UserManager<UserEntity>>();
                var roleNamager = service.GetRequiredService<RoleManager<RoleEntity>>();


                context.Database.Migrate();
                if (!context.Categories.Any())
                {
                    CategoryEntity categoryEntity = new CategoryEntity()
                    {
                        Name = "New Category",
                        Image = "No-image-found.jpg",
                        Priority = 0,
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                        Description = "Description"
                    };
                    context.Categories.Add(categoryEntity);
                    context.SaveChanges();
                }
                if(!context.Roles.Any())
                {
                    foreach(string name in Roles.All)
                    {
                        var roleEntity = new RoleEntity
                        {
                            Name = name
                        };
                        var result = roleNamager.CreateAsync(roleEntity).Result;
                    }
                }
                if (!context.Users.Any())
                {
                    
                        var userEntity = new UserEntity()
                        {
                            FirstName = "Саша",
                            LastName = "Гусак",
                            Email = "admin@gmail.com",
                            UserName = "admin",
                        };
                        var result = userNamager.CreateAsync(userEntity, "123456").Result;
                    if (result.Succeeded)
                    {
                        result = userNamager.AddToRoleAsync(userEntity, Roles.Admin).Result;
                    }
                }
                if (!context.Countries.Any())
                {
                    var Country = new CountryEntity()
                    {
                        Name = "Україна",
                    };
                    context.Countries.Add(Country);
                    context.SaveChanges();

                    var Region = new RegionEntity()
                    {
                        Name = "Волинь",
                        Country = Country
                    };
                    context.Regiones.Add(Region);
                    context.SaveChanges();

                    var City = new CityEntity()
                    {
                        Name = "Луцьк",
                        Region = Region
                    };
                    context.Cities.Add(City);
                    context.SaveChanges();

                    var Delivery = new DeliveryEntity()
                    {
                        Name = "НоваПочта"
                    };
                    context.Deliveries.Add(Delivery);
                    context.SaveChanges();

                    var Post = new PostOfficeEntity()
                    {
                        PostIndex = "4444",
                        Name = "post #1",
                        Street ="Glushets 1",
                        City = City,
                        Delivery = Delivery
                    };
                    context.PostOffices.Add(Post);
                    context.SaveChanges();
                }
                if (!context.OrderStatus.Any())
                {
                    var Order = new OrderStatusEntity()
                    {
                        Name = "Успішно"
                    };
                    context.OrderStatus.Add(Order);
                    context.SaveChanges();
                    var Order1 = new OrderStatusEntity()
                    {
                        Name = "Відхилено"
                    };
                    context.OrderStatus.Add(Order1);
                    context.SaveChanges();
                    var Order2 = new OrderStatusEntity()
                    {
                        Name = "В дорозі"
                    };
                    context.OrderStatus.Add(Order2);
                    context.SaveChanges();
                }
                if(!context.PayMethod.Any())
                {
                    var PayMethod = new PayMethodEntity()
                    {
                        Name = "Наложний платіж",
                    };
                    context.PayMethod.Add(PayMethod);
                    context.SaveChanges();
                    var PayMethod1 = new PayMethodEntity()
                    {
                        Name = "Оплата картою",
                    };
                    context.PayMethod.Add(PayMethod1);
                    context.SaveChanges();
                }
                if(!context.PayStatus.Any())
                {
                    var PayStatus = new PayStatusEntity()
                    {
                        Name = "Проплачено"
                    };
                    context.PayStatus.Add(PayStatus);
                    context.SaveChanges();
                    var PayStatus1 = new PayStatusEntity()
                    {
                        Name = "Не оплачено"
                    };
                    context.PayStatus.Add(PayStatus1);
                    context.SaveChanges();
                }

            }
        }
    }
}
