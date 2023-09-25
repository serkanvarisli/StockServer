namespace StockServer.Entities
{
 
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        // one to many
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //many to many
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
