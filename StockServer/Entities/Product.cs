namespace StockServer.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public int Stock  { get; set; }
    }
}
