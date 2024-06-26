using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Common.Interfaces.Authentication;
using OnlineStore.Application.Common.Interfaces.Services;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using OnlineStore.Application.Common.Interfaces.Persistence;
using OnlineStore.Infrastructure.Persistence;

namespace OnlineStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}