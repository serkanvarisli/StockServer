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
using StockServer.Token;
using StockServer.DTOs.UserDTOs;

namespace StockServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly StockDbContext _stockDbContext;
        private readonly ITokenHandler _tokenHandler;
        public LoginController(StockDbContext stockDbContext, ITokenHandler tokenHandler)
        {
            _stockDbContext = stockDbContext;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto model)
        {
            var user = _stockDbContext.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || model.Password != user.Password)
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı.");
            }
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Username),
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                };
            var token = _tokenHandler.CreateAccessToken(7, authClaims);

            token.UserId = user.Id;
            token.Username = user.Username;

            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
