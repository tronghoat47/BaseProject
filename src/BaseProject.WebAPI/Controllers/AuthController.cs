using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Constants;
using BaseProject.Domain.Models;
using BaseProject.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [ApiController]
    [Route("auths")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public AuthController(IAuthService authService, ICookieService cookieService)
        {
            _authService = authService;
            _cookieService = cookieService;
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
                var (token, refreshToken) = await _authService.LoginAsync(request.Email, request.Password);
                var response = new GeneralResponse
                {
                    Message = "User logged in successfully",
                    Data = new { token, refreshToken }
                };
                _cookieService.SetCookie("jwt", token, null, 30);
                _cookieService.SetCookie("refreshToken", refreshToken.TokenHash, 7, null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                var (newToken, newRefreshToken) = await _authService.RefreshTokenAsync(refreshToken);
                _cookieService.SetCookie("jwt", newToken, null, 30);
                _cookieService.SetCookie("refreshToken", newRefreshToken.TokenHash, 7, null);
                var response = new GeneralResponse
                {
                    Message = "Token refreshed successfully",
                    Data = new { newToken, newRefreshToken }
                };
                return Ok(response);
            }
            var responseError = new GeneralResponse
            {
                Message = "Refresh token not found"
            };
            return BadRequest(responseError);
        }

        [HttpGet("test-auth")]
        [Authorize(Roles = "user")]
        public IActionResult TestAuth()
        {
            return Ok("You are authorized");
        }
    }
}