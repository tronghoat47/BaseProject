using BaseProject.Infrastructure.Services.Core;
using BaseProject.Infrastructure.Services.Impl;
using BaseProject.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}