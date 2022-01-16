using InventoryTracker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //var root = Directory.GetCurrentDirectory();
                    //var dotenv = Path.Combine(root, ".env");
                    //DotEnv.Load(dotenv);

                    //var config = new ConfigurationBuilder()
                    //    .AddEnvironmentVariables()
                    //    .Build();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
