using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System.Threading.Tasks;
using Infrastructure.Models;
using System.Collections.Generic;

namespace ElasticSearchSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly ElasticClient _client;

        private const string IndexName = "employee";

        public PersonController(ElasticClient client, ILoggerFactory loggerFactory)
        {   
            _client = client;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        /// <summary>
        /// 创建或更新整个文档
        /// 更新文档要根据某个id进行更新
        /// <remarks>文档的更新是整个数据更新，不能只更新某个字段，否则其他字段的数据会丢失</remarks>
        /// </summary>
        /// <param name="person">文档数据</param>
        /// <param name="id">文档的id</param>
        [HttpPut]
        public async ValueTask CreateOrUpdateAsync(Person person, string id)
        {
            IndexResponse response;
            if (string.IsNullOrWhiteSpace(id))
            {
                response = await _client.IndexAsync(person, i => i.Index(IndexName));
            }
            else
            {
                response = await _client.IndexAsync(person, i => i.Index(IndexName).Id(id));
            }
            
            if (!response.IsValid)
                _logger.LogError(response.DebugInformation);
            else 
                _logger.LogInformation(response.Id);
        }

        [HttpPatch]
        public async ValueTask<Result> UpdateAsync(Person person, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var reponse = await _client.UpdateAsync<Person>(id: id, p => p.Doc(new Person()
            {
                NickName = person.NickName
            }));
            
            if (!reponse.IsValid)
                _logger.LogError(reponse.DebugInformation);

            return reponse.Result;
        }

        /// <summary>
        /// 获取所有员工
        /// </summary>
        /// <returns>所有员工</returns>
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPersonAsync()
        {
            var response = await _client.SearchAsync<Person>(s => s.Index(IndexName));
            
            if (!response.IsValid)
                _logger.LogError(response.DebugInformation);

            return response.Documents;
        }

        /// <summary>
        /// 按关键字查询员工
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>关键字匹配的员工</returns>
        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<Person>> GetPersonAsync(string keyword)
        {
            var response = await _client.SearchAsync<Person>(c => 
            {
                return c.Index(IndexName).Query(q => 
                {
                    return q.Bool(b => b.Should(
                        s => s.Match(m => m.Analyzer("ik_max_word").Field(p => p.NickName).Query(keyword)),
                        s => s.Match(m => m.Field(p => p.Name).Query(keyword)),
                        s => s.Match(m => m.Analyzer("ik_max_word").Field(p => p.Job.Department).Query(keyword)),
                        s => s.Match(m => m.Analyzer("ik_max_word").Field(p => p.Job.Name).Query(keyword)))
                    );
                });
            });

            if (!response.IsValid)
                _logger.LogError(response.DebugInformation);
            
            return response.Documents;

        }
    }
}