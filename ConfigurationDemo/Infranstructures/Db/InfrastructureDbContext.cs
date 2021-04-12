using Microsoft.EntityFrameworkCore;
using System;

namespace ConfigurationDemo.Infranstructures.Db
{
    public class InfrastructureDbContext : DbContext
    {
        private readonly string _connectionString;

        public InfrastructureDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) 
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}