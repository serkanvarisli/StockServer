using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockServer.Contexts;

namespace StockServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly StockDbContext _stockDbContext;
        public CategoriesController(StockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _stockDbContext.Categories
                .ToListAsync();
            return Ok(categories);
        }
    }
}
