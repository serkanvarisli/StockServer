using StockServer.DTOs.ProductDTOs;
using System.Security.Claims;

namespace StockServer.Token;

public interface ITokenHandler
{
    TokenDto CreateAccessToken(int minute, List<Claim>? authClaims);
}