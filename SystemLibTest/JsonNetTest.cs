using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Infrastructure.Extensions;

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
            var jsonString = JsonSerializer.Serialize(testModel);
            var resultModel = JsonSerializer.Deserialize<Model>(jsonString);
        //Then
            Assert.Equal(testModel.Timeout, resultModel.Timeout);
        }

        [Fact]
        public async ValueTask JsonToDictionary()
        {
        //Given
            var stream = File.OpenRead("test.json");
        //When
            var data = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(stream);
        //Then
            Assert.True(data.Values.All(v => v is JsonElement));
        }

        [Fact]
        public async ValueTask JsonElementToDictionary()
        {
        //Given
            var stream = File.OpenRead("test.json");
            var jsonDocument = await JsonDocument.ParseAsync(stream);
        //When
            var data = jsonDocument.RootElement.ToKeyValuePairs();
        //Then
            Assert.True(data is Dictionary<string, string>);
        }
    }

    public class Model
    {
        public TimeSpan Timeout { get; set; }
    }
}