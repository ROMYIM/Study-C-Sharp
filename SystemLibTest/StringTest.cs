using Xunit;

namespace SystemLibTest
{
    public class StringTest
    {
        [Theory]
        [InlineData("Fiscalização aduaneira finalizada, CURITIBA / PR")]
        public void UpperTest(string testString)
        {
        //Given
        
        //When
            var upperText = testString.ToUpper();
        //Then
            Assert.Equal("FISCALIZAÇÃO ADUANEIRA FINALIZADA, CURITIBA / PR", upperText);
        }
    }
}