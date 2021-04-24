using Microsoft.Extensions.Configuration;
using ConfigurationDemo.Infranstructures.Db;
using System;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    public class PgSqlConfigurationSource : IConfigurationSource
    {
        public DbContextOptions<InfrastructureDbContext> DbOptions { get;}

        public PgSqlConfigurationSource(Action<DbContextOptionsBuilder<InfrastructureDbContext>> setupOptions)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InfrastructureDbContext>();
            setupOptions(optionsBuilder);
            DbOptions = optionsBuilder.Options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            throw new System.NotImplementedException();
        }
    }
}