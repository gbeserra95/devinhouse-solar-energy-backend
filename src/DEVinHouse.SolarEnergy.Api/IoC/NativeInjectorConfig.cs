using DEVinHouse.SolarEnergy.Application.Interfaces.Services;
using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Identity.Data;
using DEVinHouse.SolarEnergy.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DEVinHouse.SolarEnergy.Api.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var conStrBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("LocalSQLserver"));
            conStrBuilder.Password = configuration["DbPassword"];
            
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(conStrBuilder.ConnectionString)
            );

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(conStrBuilder.ConnectionString)
            );

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<AuthMessageSenderOptions>(configuration);
        }
    }
}