using System;
using System.Net;
using System.Threading.Tasks;
using WebHostSample.Http;

namespace WebHostSample.Server
{
    public class HttpLinsetenerServer : IServer
    {
        private readonly HttpListener _listener;

        private readonly string[] _urls;

        public HttpLinsetenerServer(params string[] urls)
        {
            _listener = new HttpListener();
            _urls = new string[] { "http://localhost:5000" };
        }

        async Task IServer.StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _listener.Prefixes.Add(url));
            _listener.Start();
            while (true)
            {
                var listenerContext = await _listener.GetContextAsync();
            }

            
        }
    }
}