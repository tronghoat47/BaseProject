using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password, int roleId);

        Task<(string token, RefreshToken refreshToken)> LoginAsync(string email, string password);

        Task<(string token, RefreshToken refreshToken)> RefreshTokenAsync(string refreshToken);

        Task LogoutAsync(string userId);

        Task<int> ResetPasswordAsync(string email, string newPassword);
    }
}