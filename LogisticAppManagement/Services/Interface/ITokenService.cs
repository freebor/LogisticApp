using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;
using System.Security.Claims;

namespace LogisticAppManagement.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrinciplesFromExpiredToken(string token);
    }
}
