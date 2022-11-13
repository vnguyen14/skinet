using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //To apply migrations and create db:
            //Remove Run() and assign the CreateHostBuilder to host
            var host = CreateHostBuilder(args).Build(); 
            //Get access to data context:
            //Since we're outside of services containers in Startup class, we don't have control over the lifetime of when we create this specific instance of our context,
            //we have to do this in a using statement.
            //A using stement means any code inside of it will be disposed as soon as we're finished with the method inside there
            using (var scope = host.Services.CreateScope())
            {
                //Get services
                var services = scope.ServiceProvider; 
                //To log contents out in console, create an instance of loggerFactory
                //A loggerFactory allows us to create instnaces of a logger class
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try 
                {
                    //Get StoreContext
                    var context = services.GetRequiredService<StoreContext>();
                    //MigrateAsync() applies any pending migrations for the context to the db, and create db if it's not already existed
                    await context.Database.MigrateAsync();
                    //Tell application to seed data. Since SeedAsync is a static method, we can call it directly without having to create an instance of the StoreContextSeed class
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    //Create an instance of a logger service, then specifies the class that we want to log against
                    var logger = loggerFactory.CreateLogger<Program>();
                    //Pass in ex and an error message
                    logger.LogError(ex, "An error occured during migration");
                }
            }
            //Call Run() again for the application to start
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
