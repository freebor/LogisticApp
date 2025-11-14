using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Services.Interface
{
    public interface IAuthService
    {
       Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
       Task<AuthResponseDto> LoginAsync(LoginDto dto);
       Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
       Task<bool> ChangePasswordAsync(Guid id, ChangePasswordDto dto);
       Task LogoutAsync(Guid userId);
       Task<User?> GetUserByIdAsync(Guid id);
       Task<User?> GetUserByEmailAsync(string email);
    }
}
