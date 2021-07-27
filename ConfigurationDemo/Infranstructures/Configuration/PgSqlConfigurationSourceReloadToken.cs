using System;
using System.Threading;
using Microsoft.Extensions.Primitives;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    internal class PgSqlConfigurationSourceReloadToken : IChangeToken
    {
        private CancellationTokenSource _tokenSource;

        private readonly Timer _timer;

        public bool ActiveChangeCallbacks => true;

        public bool HasChanged => _tokenSource.IsCancellationRequested;

        public PgSqlConfigurationSourceReloadToken()
        {
            _tokenSource = new CancellationTokenSource();
            _timer = new Timer(_ =>
            {
                Interlocked.Exchange(ref _tokenSource, new CancellationTokenSource()).Cancel();
            }, _tokenSource, 10000, 1000 * 10);
        }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            return _tokenSource.Token.Register(callback, state);
        }
    }
}