using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebShop.Constants;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Identity;

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
            }
        }
    }
}
