using System.IO;
using System.Collections.Specialized;
using WebHostSample.Features;
using System.Text;

namespace WebHostSample.Http
{
    public class HttpResponse : IResponseFeature
    {
        public int StautsCode { get; set; }

        public NameValueCollection Headers { get; set; }

        public Stream Body { get; set; }

        public int StatusCode { get; set;}
        
        public Encoding ContentEncoding { get; set; } = Encoding.UTF8;
    }
}