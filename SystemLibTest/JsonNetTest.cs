using System;
using System.Threading;
using Xunit;

namespace SystemLibTest
{
    public class JsonNetTest
    {

        private const string jsonText = "{ \"time\" :\"/Date(1608767700000)/\"}";

        [Fact]
        public void DateTimeDeserialize()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public void TimeSpanSerialize()
        {
        //Given
            var testModel = new Model
            {
                Timeout = TimeSpan.FromMinutes(3)
            };
        //When
            var jsonString = System.Text.Json.JsonSerializer.Serialize(testModel);
            var resultModel = System.Text.Json.JsonSerializer.Deserialize<Model>(jsonString);
        //Then
            Assert.Equal(testModel.Timeout, resultModel.Timeout);
        }
    }

    public class Model
    {
        public TimeSpan Timeout { get; set; }
    }
}