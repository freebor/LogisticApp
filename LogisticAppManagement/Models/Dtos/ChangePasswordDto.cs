using System.ComponentModel.DataAnnotations;

namespace LogisticAppManagement.Models.Dtos
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Old Password is Required")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New Password is Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "New Password should be above 6 characters")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("NewPassword",ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}