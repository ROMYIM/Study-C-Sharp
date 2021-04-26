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
    }
}