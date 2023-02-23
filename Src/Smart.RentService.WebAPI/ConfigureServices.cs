using Microsoft.OpenApi.Models;
using Smart.RentService.WebAPI.Filters.Authorization;
using Smart.RentService.WebAPI.Middleware;

namespace Smart.RentService.WebAPI
{
    public static class ConfigureServices
    {

        public static IServiceCollection AddWebUI(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<ApiKeyAuthorizationFilter>())
                .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

            services.AddSwagger();
            services.AddEndpointsApiExplorer();

            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(type => type.ToString());
                o.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Name = "X-API-Key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "API key needed to access the endpoints",
                    Scheme = "ApiKeyScheme"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
