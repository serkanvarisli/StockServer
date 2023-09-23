namespace StockServer.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> CategoryIds { get; set; }

        public int Stock { get; set; }
    }
}
