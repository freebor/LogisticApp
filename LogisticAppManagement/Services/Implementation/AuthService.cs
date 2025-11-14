using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using LogisticAppManagement.Services.Interface;
using System.Security.Claims;
using BCrypt.Net;
using LogisticAppManagement.Exceptions;
using LogisticAppManagement.Repository.Implementation;

namespace LogisticAppManagement.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUnitOfWork uow, ITokenService tokenService, ILogger<AuthService> logger)
        {
            _uow = uow;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            // Validate password confirmation
            if (dto.Password != dto.ConfirmPassword)
                throw new BusinessException("Password and confirmation password do not match");

            // Check if email already exists
            var emailExists = await _uow.Users.EmailExistsAsync(dto.EmailAddress);
            if (emailExists)
                throw new BusinessException($"Email '{dto.EmailAddress}' is already registered");

            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Create user
            var user = new User
            {
                Email = dto.EmailAddress,
                Password= passwordHash,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role,
                IsActive = true,
                EmailConfirmed = false
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("New user registered: {Email} with role {Role}", user.Email, user.Role);

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Store refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiredAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            // Find user by email
            var user = await _uow.Users.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new BusinessException("Invalid email or password");

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new BusinessException("Invalid email or password");

            // Check if account is active
            if (!user.IsActive)
                throw new BusinessException("Your account has been deactivated. Please contact support.");

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Store refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.LastLoginAt = DateTime.UtcNow;
            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("User logged in: {Email}", user.Email);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiredAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            // Validate access token
            var principal = _tokenService.GetPrinciplesFromExpiredToken(dto.AccessToken);
            if (principal == null)
                throw new BusinessException("Invalid access token");

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new BusinessException("Invalid token claims");

            var userId = Guid.Parse(userIdClaim.Value);

            // Find user with refresh token
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user == null || user.RefreshToken != dto.RefreshToken)
                throw new BusinessException("Invalid refresh token");

            // Check if refresh token is expired
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new BusinessException("Refresh token has expired");

            // Generate new tokens
            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update refresh token
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("Tokens refreshed for user: {Email}", user.Email);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiredAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password))
                throw new BusinessException("Current password is incorrect");

            // Hash new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.LastLoginAt = DateTime.UtcNow;

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("Password changed for user: {Email}", user.Email);
            return true;
        }

        public async Task LogoutAsync(Guid userId)
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            // Clear refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("User logged out: {Email}", user.Email);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _uow.Users.GetByIdAsync(userId);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _uow.Users.GetByEmailAsync(email);
        }
    }
}

