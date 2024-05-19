using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);

        RefreshToken GenerateRefreshToken();
    }
}