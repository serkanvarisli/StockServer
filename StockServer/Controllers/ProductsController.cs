using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockServer.Contexts;
using StockServer.DTOs.ProductDTOs;
using StockServer.Entities;

namespace StockServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StockDbContext _stockDbContext;
        public ProductsController(StockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _stockDbContext.Products
                .Include(p=>p.Category)
                .Where(p => p.Stock != default)
                .Select(p =>new GetAllProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description= p.Description,
                    Stock= p.Stock,
                    TagName = p.Tags.Select(c => c.Name).ToList(),
                    CategoryName=p.Category.Name,
                } )
                .ToListAsync();

            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            var tags = await _stockDbContext.Tags.Where(c => createProductDTO.TagNames.Contains(c.Name)).ToListAsync();

            var addProduct = new Product
            {
                CategoryId = createProductDTO.CategoryId,
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Stock = createProductDTO.Stock,
                Tags = tags,
            };

            await _stockDbContext.Products.AddAsync(addProduct);

            await _stockDbContext.SaveChangesAsync();

            return StatusCode(201,addProduct.Id);
        }

        [HttpPut("updatestock/{id}")]
        public async Task<IActionResult> UpdateStock(Guid id, UpdateProductDTO updateStockDTO)
        {
            try
            {
                // İlgili ürünü veritabanından bulun
                var product = await _stockDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                // Eğer ürün bulunamazsa, hata mesajı döndürün
                if (product == null)
                {
                    return NotFound("Ürün bulunamadı.");
                }

                // Stok güncellemesi yapın
                product.Stock = updateStockDTO.Stock;

                // Veritabanını güncelleyin
                await _stockDbContext.SaveChangesAsync();

                return Ok(product); // Güncellenen ürün bilgisini döndürün
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata: {ex.Message}");
            }
        }
    }

}

