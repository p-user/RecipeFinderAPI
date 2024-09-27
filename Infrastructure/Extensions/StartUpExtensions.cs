using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Infrastructure.Extensions
{
    public static class StartUpExtensions
    {

        public static async void CreateDbIfNotExists(this IHost host)
        {

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<ApiDbContext>();

            //create db 
            try
            {

                var database = dbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                //if (database != null)
                //{
                //    if (!database.CanConnect())
                //    {
                //        database.Create();
                //    }

                //    dbContext.Database.Migrate();


                //}

                await SeedData.Initialize(services);
            }
            catch (Exception ex) 
            { 

            }

        }
    }
}
