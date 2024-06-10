using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password, int roleId);

        Task<(string token, string refreshToken, string role, string userId)> LoginAsync(string email, string password);

        Task<(string token, string refreshToken, string role, string userId)> RefreshTokenAsync(string refreshToken);

        Task<int> LogoutAsync(string userId);

        Task<int> ResetPasswordAsync(string email, string newPassword, string confirmPassword);
    }
}