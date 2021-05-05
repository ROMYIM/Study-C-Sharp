using Xunit;
using System.Text.RegularExpressions;

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
    }
}