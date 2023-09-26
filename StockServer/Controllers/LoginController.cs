﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = _stockDbContext.Login.FirstOrDefault(u => u.Username == model.Username);
            if (user != null && model.Username == user.Username && model.Password == user.Password)
            {
                await HttpContext.SignOutAsync();

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Username));


                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal, new AuthenticationProperties() { IsPersistent = false });

                return StatusCode(200,model.Username);
            }

            return BadRequest();
        }

    }
}