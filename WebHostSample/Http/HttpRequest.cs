using System.Collections.Specialized;
using System.IO;
using WebHostSample.Features;

namespace WebHostSample.Http
{
    public class HttpRequest : IRequestFeature
    {
        public NameValueCollection Headers { get; set; }

        public Stream Body { get; set; }

        public string HttpMethod { get; set; }
        
        public NameValueCollection QueryString { get; set; }
    }
}