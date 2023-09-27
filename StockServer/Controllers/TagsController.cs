using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockServer.Contexts;

namespace StockServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TagsController : Controller
    {
        private readonly StockDbContext _stockDbContext;
        public TagsController(StockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _stockDbContext.Tags
                .ToListAsync();
            return Ok(tags);
        }
    }
}
