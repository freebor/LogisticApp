using LogisticAppManagement.Models.Enums;

namespace LogisticAppManagement.Models.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime LastLoginAt { get; set; } 


        public Guid? DriverId { get; set; }
        public Driver? Driver { get; set; }

        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
