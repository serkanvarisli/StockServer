namespace StockServer.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
