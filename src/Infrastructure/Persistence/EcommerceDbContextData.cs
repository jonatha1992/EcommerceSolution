using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ecommerce.Infrastructure.Persistence;


public class EcommerceDbContextData
{
    public static async Task LoadDataAsync(
        EcommerceDbContext context,
        UserManager<User> usuarioManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if(!usuarioManager.Users.Any())
            {
                var usuarioAdmin = new User
                {
                    Name = "Jonathan",
                    Surname = "Correa",
                    Email = "Jonathancorrea291292@hotmail.com",
                    UserName = "Joni.correa",
                    Telephone = "12345678",
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/ecommerce-199d2.appspot.com/o/Foto%20Carnet.jpg?alt=media&token=49d5c120-38be-484a-a4e8-7dfd6ddbf8d0",
                };
                await usuarioManager.CreateAsync(usuarioAdmin, "Micontraseña1234!");
                await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);

                var usuario = new User
                {
                    Name = "Juan",
                    Surname = "Perez",
                    Email = "juan.perez@gmail.com",
                    UserName = "juan.perez",
                    Telephone = "98563434534",
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d",
                };
                await usuarioManager.CreateAsync(usuario, "Micontraseña1234!");
                await usuarioManager.AddToRoleAsync(usuario, Role.USER);

            }

            if(!context.Categories!.Any())
            {
                var categoryData = File.ReadAllText("../Infrastructure/Data/category.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                await context.Categories!.AddRangeAsync(categories!);
                await context.SaveChangesAsync();
            }

            if (!context.Products!.Any())
            {
                var productData = File.ReadAllText("../Infrastructure/Data/product.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                await context.Products!.AddRangeAsync(products!);
                await context.SaveChangesAsync();
            }

            if (!context.Images!.Any())
            {
                var imageData = File.ReadAllText("../Infrastructure/Data/image.json");
                var imagenes = JsonConvert.DeserializeObject<List<Image>>(imageData);
                await context.Images!.AddRangeAsync(imagenes!);
                await context.SaveChangesAsync();
            }

            if (!context.Reviews!.Any())
            {
                var reviewData = File.ReadAllText("../Infrastructure/Data/review.json");
                var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData);
                await context.Reviews!.AddRangeAsync(reviews!);
                await context.SaveChangesAsync();
            }


            if (!context.Countries!.Any())
            {
                var countryData = File.ReadAllText("../Infrastructure/Data/countries.json");
                var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                await context.Countries!.AddRangeAsync(countries!);
                await context.SaveChangesAsync();
            }
        
        }
        catch(Exception e)
        {
            var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
            logger.LogError(e.Message);
        }

    }
    
}
