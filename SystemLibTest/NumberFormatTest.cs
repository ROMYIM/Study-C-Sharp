using Xunit;

namespace SystemLibTest
{
    public class NumberFormatTest
    {
        [Fact]
        public void DeciamlN2Test()
        {
        //Given
            decimal d = 2.33333333M;
        //When
            var s = d.ToString("N3");
        //Then
            Assert.Equal("2.333", s);
        }
    }
}