namespace StockServer.Entities
{
    public class Category
    {
        //one to many
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
