using System.ComponentModel.DataAnnotations.Schema;

namespace StockServer.Entities
{
    public class Category
    {
        //one to many
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
