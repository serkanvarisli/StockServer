namespace StockServer.Entities
{
    //many to many
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
