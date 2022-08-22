using System.Reflection;
using Microsoft.OpenApi.Models;

namespace DEVinHouse.SolarEnergy.Api.Extensions
{
    public static class SwaggerSetup
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DEVinHouse.SolarEnergy.Api",
                    Description = "DEVinHouse Solar Energy is an energy management app developed in React, Typescript and ASP.Net Core\nin order to test and enhance my skills as a FullStack Developer.",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriel Beserra",
                        Url = new Uri("https://gabrielbeserra.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/gbeserra95/devinhouse-solar-energy-backend/License.txt")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                    Enter 'Bearer' [space] and then your token in the text input below. 
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}