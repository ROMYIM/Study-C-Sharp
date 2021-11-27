using System;
using System.Collections.Generic;
using Xunit;

namespace SystemLibTest
{
    public class UriTest
    {
        [Theory]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample/")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/wcfsample")]
        public void TestEqual(string uri1, string uri2)
        {
            var uri3 = new Uri(uri1);
            var uir4 = new Uri(uri2);
            
            Assert.Equal(uri3, uir4);
        }
        
        [Theory]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample/")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/wcfsample")]
        public void TestCompare(string uri1, string uri2)
        {
            var uri3 = new Uri(uri1);
            var uir4 = new Uri(uri2);

            var comparer = Comparer<Uri>.Default;
            Assert.Equal(0, comparer.Compare(uri3, uir4));
        }

        [Theory]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample/")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/wcfsample")]
        public void TestAbsolutePathEqual(string uri1, string uri2)
        {
            var uri3 = new Uri(uri1);
            var uir4 = new Uri(uri2);
            
            Assert.Equal(uri3.AbsolutePath, uir4.AbsolutePath);
        }
        
        [Theory]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample/")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/WcfSample")]
        [InlineData("http://localhost:8733/wcfsample/", "http://localhost:8733/wcfsample")]
        public void TestAbsoluteUriEqual(string uri1, string uri2)
        {
            var uri3 = new Uri(uri1);
            var uir4 = new Uri(uri2);
            
            Assert.Equal(uri3.AbsoluteUri, uir4.AbsoluteUri);
        }
    }
}