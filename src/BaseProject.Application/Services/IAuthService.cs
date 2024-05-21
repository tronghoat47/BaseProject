using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password, int roleId);

        Task<(string token, string refreshToken, string role)> LoginAsync(string email, string password);

        Task<(string token, string refreshToken, string role)> RefreshTokenAsync(string userId, string refreshToken);

        Task<int> LogoutAsync(string userId);

        Task<int> ResetPasswordAsync(string email, string newPassword);
    }
}