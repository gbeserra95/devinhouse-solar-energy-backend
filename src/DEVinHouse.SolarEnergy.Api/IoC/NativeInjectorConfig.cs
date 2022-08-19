using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Data.Repositories;
using DEVinHouse.SolarEnergy.Data.Repositories.Shared;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;
using DEVinHouse.SolarEnergy.Domain.Services;
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

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPlantRepository, PlantRepository>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IPlantService, PlantService>();
            
            services.Configure<AuthMessageSenderOptions>(configuration);
        }
    }
}