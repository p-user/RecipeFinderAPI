using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApiDbContext>(
                    (sp, opt) =>
                    {
                        opt.UseSqlServer(connectionString);
                        opt.EnableSensitiveDataLogging();
                        opt.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>());
                        opt.AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>());
                    });

            services.AddIdentityApiEndpoints<ApplicationUser>()
                      .AddRoles<IdentityRole>()
                      .AddEntityFrameworkStores<ApiDbContext>();
           
                   

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IMeasurementUnitsRepository, MeasurementUnitsRepository>();
            services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            services.AddSingleton<UpdateAuditableInterceptor>();
            services.AddSingleton<SoftDeleteInterceptor>();
            services.AddScoped<IUserContext, UserContext>();
          



        }
    }
}
