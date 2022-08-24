using System.Text;
using DEVinHouse.SolarEnergy.Domain.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DEVinHouse.SolarEnergy.Api.Extensions
{
    public static class AuthenticationSetup
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingsOptions = configuration.GetSection(nameof(JwtOptions));
            var sendGridOptions = configuration.GetSection(nameof(AuthMessageSenderOptions));
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value));
            
            services.Configure<JwtOptions>(options => 
            {
                options.Issuer = jwtAppSettingsOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtAppSettingsOptions[nameof(JwtOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                options.Expiration = int.Parse(jwtAppSettingsOptions[nameof(JwtOptions.Expiration)] ?? "0");
            });

            services.Configure<AuthMessageSenderOptions>(options => 
            {
                options.SendGridName = sendGridOptions[nameof(AuthMessageSenderOptions.SendGridName)];
                options.SendGridEmail = sendGridOptions[nameof(AuthMessageSenderOptions.SendGridEmail)];
                options.ConfirmEmailTemplateId = sendGridOptions[nameof(AuthMessageSenderOptions.ConfirmEmailTemplateId)];
                options.ResetPasswordEmailTemplateId = sendGridOptions[nameof(AuthMessageSenderOptions.ResetPasswordEmailTemplateId)];
                options.SendGridKey = sendGridOptions[nameof(AuthMessageSenderOptions.SendGridKey)];
            });

            services.Configure<IdentityOptions>(options => 
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}