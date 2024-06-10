using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.Helpers;
using BaseProject.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly IEmailService _emailService;

        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, ICryptographyHelper cryptographyHelper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _cryptographyHelper = cryptographyHelper;
            _emailService = emailService;
        }

        public async Task<User> RegisterAsync(string email, string password, int roleId)
        {
            var userExist = await _unitOfWork.UserRepository.GetAsync(u => u.Email == email);
            if (userExist != null)
            {
                throw new InvalidOperationException("User already exists");
            }
            var salt = _cryptographyHelper.GenerateSalt();
            var hashedPassword = _cryptographyHelper.HashPassword(password, salt);

            var user = new User
            {
                Email = email,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                Status = StatusUsersConstants.IN_ACTIVE,
                RoleId = (byte)roleId
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                var isSuccess = await _emailService.SendEmailAsync(user.Email, EmailConstants.SUBJECT_ACTIVE_ACCOUNT, EmailConstants.BodyActivationEmail(email));
                if (!isSuccess)
                {
                    throw new InvalidOperationException("Failed to send email");
                }
            }
            return user;
        }

        public async Task<(string token, string refreshToken, string role, string userId)> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == email, u => u.Role);
            if (user == null || !_cryptographyHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Email or password incorrect!!!");
            }

            if (user.Status != StatusUsersConstants.ACTIVE)
            {
                throw new UnauthorizedAccessException("Account is not activated");
            }

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            refreshToken.UserId = user.Id;

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.CommitAsync();

            return (token, refreshToken.TokenHash, user.Role.Name, user.Id);
        }

        public async Task<(string token, string refreshToken, string role, string userId)> RefreshTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetAsync(rt => rt.TokenHash == refreshToken)
                .ConfigureAwait(false);
            if (token == null || token.ExpiredAt <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == token.UserId, u => u.Role);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var newJwtToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            newRefreshToken.UserId = user.Id;

            _unitOfWork.RefreshTokenRepository.Delete(token);
            await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);
            await _unitOfWork.CommitAsync();

            return (newJwtToken, newRefreshToken.TokenHash, user.Role.Name, user.Id);
        }

        public async Task<int> LogoutAsync(string userId)
        {
            var tokens = await _unitOfWork.RefreshTokenRepository.GetAllAsync(rt => rt.UserId == userId);
            _unitOfWork.RefreshTokenRepository.RemoveRange(tokens);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> ResetPasswordAsync(string email, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                throw new InvalidOperationException("Passwords do not match");
            }
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var passwordSalt = _cryptographyHelper.GenerateSalt();
            var passwordHash = _cryptographyHelper.HashPassword(newPassword, passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            return await _unitOfWork.CommitAsync();
        }
    }
}