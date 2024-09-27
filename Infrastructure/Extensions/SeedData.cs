using Application.Constants;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var measurementUnits = serviceProvider.GetRequiredService<IMeasurementUnitsRepository>();


            string[] roleNames = { GlobalConstants.GuestUser, GlobalConstants.RoleUser, GlobalConstants.AdminUser };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var admin = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
            };

            string adminPassword = "Admin123!";
            var _user = await userManager.FindByEmailAsync("admin@admin.com");

            if (_user == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, GlobalConstants.AdminUser);
                }
            }

            string[] defaultMeasures = { "gr", "kg", "tbsp", "tsp", "fl oz", "ml", "l", "stick", "piece", "pack", "pinch", "drop" };
            foreach (var unit in defaultMeasures)
            {
                var unitExists = await measurementUnits.FindBySymbol(unit.ToUpper());
                if (unitExists is null)
                {
                    var entity = await measurementUnits.AddAsync(new MeasurementUnit() { Symbol = unit });
                    await measurementUnits.SaveChangesAsync();
                }
            }

            //string[] defaultCategories = { "Breakfast", "Lunch", "Dinner", "Snacks", "Beverages", "Vegan", "Chicken" };
            //foreach (var unit in defaultCategories)
            //{
            //    var unitExists = await categories.FindByName(unit);
            //    if (unitExists is null)
            //    {
            //        var entity = await categories.AddAsync(new Category() { Name = unit });
            //        await measurementUnits.SaveChangesAsync();
            //    }
            //}


        }
    }
}
