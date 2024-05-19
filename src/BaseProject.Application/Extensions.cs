using BaseProject.Application.Services;
using BaseProject.Application.Services.Impl;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.Helpers;
using BaseProject.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Application
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptographyHelper, CryptographyHelper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}