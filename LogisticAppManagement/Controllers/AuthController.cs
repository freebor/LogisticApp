using LogisticAppManagement.Common;
using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LogisticAppManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse("Validation failed",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            }

            var response = await _authService.RegisterAsync(dto);
            return Ok(ApiResponse<object>.SuccessfulResponse(response, "Registeration Successful"));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse("Validation Failed", 
                    ModelState.Values.SelectMany(v => v.Errors).Select(c => c.ErrorMessage).ToList()));
            }
            var response = await _authService.LoginAsync(dto);
            return Ok(ApiResponse<object>.SuccessfulResponse(response, "Login Successful"));
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse("Validation Faild", 
                    ModelState.Values.SelectMany(v => v.Errors).Select(c => c.ErrorMessage).ToList()));
            }

            var response = await _authService.RefreshTokenAsync(dto);
            return Ok(ApiResponse<object>.SuccessfulResponse(response, "Token Refreshed Successfully"));
        }

        [HttpPost("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse("", 
                    ModelState.Values.SelectMany(v => v.Errors).Select(c => c.ErrorMessage).ToList()));
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return BadRequest(ApiResponse<object>.FailureResponse("User not authenticated", "Unauthorized"));
            }

            var userId = Guid.Parse(userIdClaim.Value); 
            await _authService.ChangePasswordAsync(userId, dto);
            return Ok(ApiResponse<object>.SuccessfulResponse(null, "Password Change Successfully"));
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
            {
                return Unauthorized(ApiResponse<object>.FailureResponse("User not authenticated", "UnAuthorized"));
            }
            var userId = Guid.Parse(userIdClaim.Value);
            await _authService.LogoutAsync(userId);

            return Ok(ApiResponse<object>.SuccessfulResponse(null, "Logout Successful"));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(ApiResponse<object>.FailureResponse("User not authenticated", "Unauthorized"));


            var userId = Guid.Parse(userIdClaim.Value);
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
                return BadRequest(ApiResponse<object>.FailureResponse("User Not Found", "Not Found"));

            var userInfo = new
            {
                user.Id,
                user.Email,
                user.FullName,
                user.PhoneNumber,
                Role = user.Role.ToString(),
                user.IsActive,
                user.EmailConfirmed,
                user.LastLoginAt,
            };

            return Ok(ApiResponse<object>.SuccessfulResponse(user,"User Retrieved Successfully"));
        }
    }
}
