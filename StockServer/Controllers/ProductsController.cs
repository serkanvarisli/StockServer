using Microsoft.AspNetCore.Authorization;
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
                .Include(p => p.Category)
                .Where(p => p.Stock != default)
                .Select(p => new GetAllProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Stock = p.Stock,
                    TagName = p.Tags.Select(c => c.Name).ToList(),
                    CategoryName = p.Category.Name,
                })
                .ToListAsync();

            return Ok(products);
        }
        [HttpGet("Category")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _stockDbContext.Categories
                .ToListAsync();
            return Ok(categories);
        }
        [HttpGet("Tags")]
        public async Task<IActionResult> GetTags()
        {
            var Tags = await _stockDbContext.Tags
                .ToListAsync();
            return Ok(Tags);
        }
        [HttpPost]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            var tags = await _stockDbContext.Tags.Where(c => createProductDTO.TagNames.Contains(c.Name)).ToListAsync();

            if (_stockDbContext.Products.Any(p => p.Stock > 0))
            {
                var addProduct = new Product
                {
                    CategoryId = createProductDTO.CategoryId,
                    Name = createProductDTO.Name,
                    Description = createProductDTO.Description,
                    Stock = createProductDTO.Stock,
                    Tags = tags,
                };

                // Sadece stok miktarı 0'dan büyükse ekleme yap
                if (addProduct.Stock > 0)
                {
                    await _stockDbContext.Products.AddAsync(addProduct);
                    await _stockDbContext.SaveChangesAsync();
                    return StatusCode(201, addProduct.Id);
                }
                else
                {
                    return StatusCode(400, "Stok miktarı 0'dan büyük olmalıdır.");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStock(Guid id, UpdateProductDTO updateStockDTO)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}
            //if (!User.IsInRole("Admin"))
            //{
            //    return Forbid();
            //}

            var product = await _stockDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return BadRequest("Ürün bulunamadı.");
            }

            // Yalnızca "Stock" alanını güncelle
            product.Stock = updateStockDTO.Stock;

            await _stockDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _stockDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            _stockDbContext.Products.Remove(product);
            await _stockDbContext.SaveChangesAsync();

            return NoContent();
        }
    }

}

