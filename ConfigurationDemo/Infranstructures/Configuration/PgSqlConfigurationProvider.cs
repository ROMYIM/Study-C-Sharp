using ConfigurationDemo.Infranstructures.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    public class PgSqlConfigurationProvider : IConfigurationProvider
    {
        private readonly PgSqlConfigurationSource _source;

        protected IEnumerable<ChannelOptions<object>> Options { get; private set; }

        public PgSqlConfigurationProvider(PgSqlConfigurationSource source)
        {
            _source = source;

            
        }

        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void Set(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGet(string key, out string value)
        {
            throw new System.NotImplementedException();
        }
    }
}