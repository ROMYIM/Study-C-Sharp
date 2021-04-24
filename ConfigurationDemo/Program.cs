using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationDemo.Infranstructures.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConfigurationDemo
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
                    webBuilder.ConfigureAppConfiguration((context, builder) =>
                    {
                        var infrastructureConnectionString = context.Configuration.GetConnectionString("pgsql");
                        builder.Add(new PgSqlConfigurationSource(optionsBuilder => optionsBuilder.UseNpgsql(infrastructureConnectionString)));
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
