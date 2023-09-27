using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockServer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StockServer.Contexts;

namespace StockServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly StockDbContext _stockDbContext;
        public LoginController(StockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User model)
        {
            var user = _stockDbContext.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user != null && model.Username == user.Username && model.Password == user.Password)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Username));

                if (user.Username != "admin")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal, new AuthenticationProperties() { IsPersistent = false });

                return StatusCode(200, model.Username);
            }
            return BadRequest();
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(User model)
        {
            await HttpContext.SignOutAsync();
            return StatusCode(200,model.Username);
        }
    }
}
