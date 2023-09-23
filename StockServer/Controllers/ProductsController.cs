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
                .Include(p=>p.Categories)
                .Where(p => p.Stock != default)
                .Select(p =>new GetAllProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description= p.Description,
                    Stock= p.Stock,
                    CategoryName = p.Categories.Select(c => c.Name).ToList()
                } )
                .ToListAsync();

            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            var categories = await _stockDbContext.Categories.Where(c => createProductDTO.CategoryIds.Contains(c.Id)).ToListAsync();

            var addProduct = new Product
            {
                Categories = categories,
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Stock = createProductDTO.Stock,
            };

            await _stockDbContext.Products.AddAsync(addProduct);

            await _stockDbContext.SaveChangesAsync();

            return StatusCode(201,addProduct.Id);
        }
    }
}
