using Microsoft.EntityFrameworkCore;
using StockServer.Contexts;
using StockServer.Entities;

namespace StockServer.SeedData
{
    public static class CreateSeedData
    {
        internal static void SeedData(StockDbContext? context)
        {
            var defaultUsers = new List<User>
            {
             new User
             {
                Id = 1,
                Password = "123",
                Role = StockServer.Enums.UserRole.Admin,
                Username = "admin"
             },
            new User
            {
                Id = 2,
                Password = "123",
                Role = StockServer.Enums.UserRole.User,
                Username = "user"
            }};

            var category = new Category
            {
                Id = 1,
                Name = "Kategori 1"
            };

            context.Users.AddRangeAsync(defaultUsers);
            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}
