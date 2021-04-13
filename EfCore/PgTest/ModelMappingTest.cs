using EfCore.DbContexts;
using EfCore.Models;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace EfCore.PgTest
{
    public class ModelMappingTest
    {
        private readonly PgDbContext _dbContext = new PgDbContext();

        [Fact]
        public async ValueTask JsonDocumentTest()
        {
        //Given
            var id = Guid.NewGuid().ToString();
            var channelOptions = new ChannelOptions<JsonDocument>
            {
                PostType = id,
                Channel = "Kjy",
                Options = JsonDocument.Parse(JsonSerializer.Serialize(new EsubOptions
                {
                    Host = "localhost:2782",
                    ClientKey = string.Empty,
                    Channel = "ES"
                }))
            };
           
            var commitCount = 0;
            try
            {
                _dbContext.Options.Add(channelOptions);
                commitCount =  _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.InnerException.Message);
            }
            


        //When
            var options = await _dbContext.Options.FindAsync("61fa656e-4068-4d44-af41-0e59aad7852f");
        //Then
            Assert.NotNull(options);
            Assert.Equal(id, options.PostType);
            Assert.Equal(1, commitCount);
        }
    }
}