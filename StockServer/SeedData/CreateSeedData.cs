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
                Username = "admin",
                Password = "123",
                Role = StockServer.Enums.UserRole.Admin
             },
            new User
            {
                Id = 2,
                Username = "user",
                Password = "123",
                Role = StockServer.Enums.UserRole.User
            }};


            var defaultCategories = new List<Category>
            {
             new Category
             {
                 Id = 1,
                Name="Kategori 1",

             },
            new Category
            {
                Id=2,
                Name="Kategori 2"
            }};
            var defaulTag = new List<Tag>
            {
             new Tag
             {
                 Id = 1,
                Name="Etiket 1",

             },
            new Tag
            {
                Id=2,
                Name="Etiket 2"
            }};

            context.Users.AddRangeAsync(defaultUsers);
            context.Categories.AddRangeAsync(defaultCategories);
            context.Tags.AddRangeAsync(defaulTag);

            context.SaveChanges();
        }
    }
}
