using System.IO;
using System.Collections.Specialized;

namespace WebHostSample.Features
{
    public interface IRequestFeature
    {
        string HttpMethod { get; set; }

        NameValueCollection QueryString { get; set; }

        NameValueCollection Headers { get; set;}

        Stream Body { get; set; }
    }
}