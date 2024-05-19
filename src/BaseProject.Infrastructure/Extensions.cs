using BaseProject.Infrastructure.Services;
using BaseProject.Infrastructure.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}