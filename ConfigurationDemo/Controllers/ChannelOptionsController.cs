using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationDemo.Infranstructures.Db;
using ConfigurationDemo.Infranstructures.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelOptionsController : ControllerBase
    {
        private readonly InfrastructureDbContext _dbContext;

        public ChannelOptionsController(InfrastructureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPatch]
        public async ValueTask<IActionResult> CreateOrEdit([FromBody]ChannelOptions options)
        {
            var originOptions = await _dbContext.ChannelOptions.FindAsync(options.PostType);
            if (originOptions != null)
            {
                originOptions.Channel = options.Channel;
                originOptions.JsonOptions = options.JsonOptions;
            }
            else
                _dbContext.Add(options);

            var commitedCount = await _dbContext.SaveChangesAsync();
            if (commitedCount > 0)
                return Ok();
            return BadRequest();
        }
    }
}