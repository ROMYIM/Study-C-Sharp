using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using EfCore.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using EfCore.Logging;

namespace EfCore.DbContexts
{
    public class PgDbContext : DbContext
    {
        private readonly string _connectionString;

        // public virtual DbSet<ChannelEsubOptions> EsubOptions { get; set; }

        public virtual DbSet<ChannelOptions<JsonDocument>> Options { get; set;}

        public PgDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true).Build();
            
            _connectionString = configuration.GetConnectionString("pgsql");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EfLoggerProvider());

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=yim;Database=configuration;Password=;");
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}