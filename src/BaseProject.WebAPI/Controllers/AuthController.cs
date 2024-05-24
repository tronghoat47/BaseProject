using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Models;
using BaseProject.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [ApiController]
    [Route("auths")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IEmailService emailService, IUserService userService)
        {
            _authService = authService;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request.Email, request.Password, request.RoleId);
                var response = new GeneralResponse
                {
                    Message = "User registered successfully",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            try
            {
                var (token, refreshToken, role) = await _authService.LoginAsync(request.Email, request.Password);
                var response = new GeneralResponse
                {
                    Message = "User logged in successfully",
                    Data = new { token, refreshToken, role }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                if (await _authService.LogoutAsync(UserID) == 0)
                {
                    throw new InvalidOperationException("User not found");
                }
                var response = new GeneralResponse
                {
                    Message = "User logged out successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("request-reset-password/{email}")]
        public async Task<IActionResult> RequestResetPasswordAsync(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }
                var isSuccess = await _emailService.SendEmailAsync(user.Email, EmailConstants.SUBJECT_RESET_PASSWORD, EmailConstants.BodyResetPasswordEmail(email));
                if (!isSuccess)
                {
                    throw new InvalidOperationException("Failed to send email");
                }
                var response = new GeneralResponse
                {
                    Message = "Reset password email sent successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] UserResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPasswordAsync(request.Email, request.Password);
                var response = new GeneralResponse
                {
                    Message = "Password reset successfully",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("request-active-account/{email}")]
        public async Task<IActionResult> RequestActiveAccountAsync(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }
                var isSuccess = await _emailService.SendEmailAsync(user.Email, EmailConstants.SUBJECT_ACTIVE_ACCOUNT, EmailConstants.BodyActivationEmail(email));
                if (!isSuccess)
                {
                    throw new InvalidOperationException("Failed to send email");
                }
                var response = new GeneralResponse
                {
                    Message = "Active account email sent successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("active-account/{email}")]
        public async Task<IActionResult> ActiveAccountAsync(string email)
        {
            try
            {
                if (await _userService.ActiveAccount(email) == 0)
                {
                    throw new InvalidOperationException("Failed to active account");
                }
                var response = new GeneralResponse
                {
                    Message = "Account activated successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var (newToken, newRefreshToken, role) = await _authService.RefreshTokenAsync(refreshTokenRequest.UserId, refreshTokenRequest.RefreshToken);
                var response = new GeneralResponse
                {
                    Message = "Token refreshed successfully",
                    Data = new { newToken, newRefreshToken, role }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet("test-auth-admin")]
        [Authorize(Roles = "admin")]
        public IActionResult TestAuth()
        {
            var response = new GeneralResponse
            {
                Message = "Test auth successfully"
            };
            return Ok(response);
        }

        [HttpGet("test-auth-all")]
        [Authorize()]
        public IActionResult TestAuthAAAa()
        {
            var response = new GeneralResponse
            {
                Message = "Test auth successfully"
            };
            return Ok(response);
        }
    }
}