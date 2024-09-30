using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using RecipeFinderAPI.Middlewares;
using Serilog;

namespace RecipeFinderAPI.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme);
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "RecipeFinder",
                    Version = "v1"
                });

                opt.AddSecurityDefinition("bearerAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter the token here",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
             new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id="bearerAuth"
                }
            },
             []

        }

    });
            });
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddHttpContextAccessor();


            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext();


            });
        }
    }
}
