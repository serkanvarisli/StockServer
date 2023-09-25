namespace StockServer.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<string> TagNames { get; set; }
        public int Stock { get; set; }
    }
}
