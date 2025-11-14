using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LogisticAppManagement.Models.Dtos
{
    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "Access Token is Required")]
        public string AccessToken { get; set; } = string.Empty;

        [Required(ErrorMessage = "Refresh Token is Required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}