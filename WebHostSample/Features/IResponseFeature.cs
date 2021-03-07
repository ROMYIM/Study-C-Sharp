using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace WebHostSample.Features
{
    public interface IResponseFeature
    {
        int StatusCode { get; set; }

        NameValueCollection Headers { get; set;}

        Stream Body { get; set; }

        Encoding ContentEncoding { get; set; }
    }
}