using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace ElasticSearchSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ElasticClient _esClient;

        private readonly ILogger _logger;

        public IndexController(ElasticClient esClient, ILoggerFactory loggerFactory)
        {
            _esClient = esClient;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        [HttpPost]
        [Route("person")]
        public async ValueTask CreatePersonIndexAsync()
        {
            var response = await _esClient.Indices.CreateAsync("employee", c => c.Map<Person>(m => m.AutoMap()));
            _logger.LogInformation(response.ToString());
        }

        [HttpGet]
        [Route("mapping")]
        public async Task GetIndexInfoAsync(string indexName)
        {
            var response = await _esClient.Indices.GetMappingAsync<Person>(c => c.Index(Indices.Index(indexName)));
            _logger.LogInformation(response.DebugInformation);
        }
    }
}