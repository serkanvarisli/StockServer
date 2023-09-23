namespace StockServer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    }
}
