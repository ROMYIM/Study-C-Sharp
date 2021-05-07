using Xunit;
using System.Text.RegularExpressions;
using System;

namespace SystemLibTest
{
    public class RegexTest
    {
        [Theory]
        [InlineData("你好9080779")]
        [InlineData("你好%%^&^*")]
        [InlineData("^&^*你好%sd%^&^*")]
        public void RemoveSpecialChars(string testString)
        {
        //Given
            var regexExpression = @"[^\u4e00-\u9fa5 ]";
        //When
            testString = Regex.Replace(testString, regexExpression, string.Empty);
        //Then
            Assert.Equal("你好", testString);
        }

        [Fact]
        public void UriTest()
        {
        //Given
            var requestUri = "http://localhost:5000/WeatherForecast/message?code=1";
        //When
            var uri = new Uri(requestUri);
        //Then
            Assert.Equal(uri.Query, "?code=1");
        }
    }
    
}