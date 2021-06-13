using System;
using Microsoft.Extensions.Primitives;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    public class PgSqlConfigurationSourceReloadToken : IChangeToken
    {
        public bool ActiveChangeCallbacks => true;

        public bool HasChanged => true;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            throw new NotImplementedException();
        }
    }
}