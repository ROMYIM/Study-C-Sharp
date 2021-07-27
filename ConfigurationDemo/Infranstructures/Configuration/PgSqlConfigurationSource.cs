using Microsoft.Extensions.Configuration;
using ConfigurationDemo.Infranstructures.Db;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    public class PgSqlConfigurationSource : IConfigurationSource
    {
        private PgSqlConfigurationSourceReloadToken _reloadToken;

        public DbContextOptions<InfrastructureDbContext> DbOptions { get;}

        public PgSqlConfigurationSource(Action<DbContextOptionsBuilder<InfrastructureDbContext>> setupOptions)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InfrastructureDbContext>();
            setupOptions(optionsBuilder);
            DbOptions = optionsBuilder.Options;
            _reloadToken = new PgSqlConfigurationSourceReloadToken();
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new PgSqlConfigurationProvider(this);
        }

        internal IChangeToken GetReloadToken() => _reloadToken;
    }
}