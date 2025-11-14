using LogisticAppManagement.Data;
using LogisticAppManagement.Repository.Implementation;
using LogisticAppManagement.Repository.Interface;
using LogisticAppManagement.Services.Implementation;
using LogisticAppManagement.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LogisticAppManagement.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LogisticsDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            // Optional: register direct access to driver repository if needed elsewhere
            services.AddScoped<IDriverRepository, DriverRepository>();

            return services;
        }
    }
}
