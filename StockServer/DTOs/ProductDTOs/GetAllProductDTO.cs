namespace StockServer.DTOs.ProductDTOs
{
    public class GetAllProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
        public List<string> TagName { get; set; }
    }
}
