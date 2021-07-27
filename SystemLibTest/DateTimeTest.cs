using System;
using Xunit;

namespace SystemLibTest
{
    public class DateTimeTest
    {
        [InlineData("/Date(1608767700000)/")]
        [Theory]
        public void DateTimeParseTes(string dateTimeString)
        {
        //Given
            var time = DateTimeOffset.FromUnixTimeMilliseconds(1608767700000);
            var testTime = DateTime.Parse(dateTimeString);
        //When
        
        //Then
            Assert.Equal(time.DateTime, testTime);
        }

        [Theory]
        [InlineData("0:0:0:59")]
        [InlineData("0:00:00:59,000")]
        [InlineData("0:0:59")]
        [InlineData("0:0:59.000")]
        public void TimeSpanParse(string spanString)
        {
        //Given
            var result = false;
        //When
            if (TimeSpan.TryParse(spanString, out var timeSpan))
                result = timeSpan == TimeSpan.FromSeconds(59);
        //Then
            Assert.True(result);
        }

        [Fact]
        public void DateTimeParseTest()
        {
        //Given
            string parrten = "yyyy-MM-ddTHH:mm:sszzz";
        //When
            var dateTimeStr = DateTime.Now.ToString(parrten);
        //Then
            System.Console.WriteLine(dateTimeStr);
        }
    }
}