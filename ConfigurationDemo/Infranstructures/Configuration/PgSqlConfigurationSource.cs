using Microsoft.Extensions.Configuration;
using ConfigurationDemo.Infranstructures.Db;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    public class PgSqlConfigurationSource : IConfigurationSource
    {
        public InfrastructureDbContext DbContext { get;}

        public PgSqlConfigurationSource(string connectionString)
        {
            DbContext = new InfrastructureDbContext(connectionString);
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            throw new System.NotImplementedException();
        }
    }
}