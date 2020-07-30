using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApp_Core.Data;

namespace WebApp_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                    var services = scope.ServiceProvider;
                    try {
                        var context = services.GetRequiredService<DataContext>();
                        context.Database.Migrate();
                        Seed.SeedParts(context);
                        System.Diagnostics.Debug.WriteLine("MIGRATED SUCCESSFULLY");
           

                    }catch(Exception e) {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(e, "Error in migrations");
                     
                    }

            };
            
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
