using Xunit;

namespace SystemLibTest
{
    public class TypeTest
    {
        [Fact]
        public void TypeNameTest()
        {
        //Given
            var typeName = GetType().ToString();
        //When
        
        //Then
            Assert.Equal("SystemLibTest.TypeTest", typeName);
        }
    }
}