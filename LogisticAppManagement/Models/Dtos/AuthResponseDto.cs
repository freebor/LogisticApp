using LogisticAppManagement.Models.Enums;

namespace LogisticAppManagement.Models.Dtos
{
    public class AuthResponseDto
    {
        public Guid UserId = Guid.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime ExpiredAt { get; set; }

    }
}