using ConfigurationDemo.Infranstructures.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using ConfigurationDemo.Infranstructures.Db;
using ConfigurationDemo.Infranstructures.Extensions;
using System.Text;

namespace ConfigurationDemo.Infranstructures.Configuration
{
    public class PgSqlConfigurationProvider : ConfigurationProvider,IDisposable
    {
        private readonly PgSqlConfigurationSource _source;

        private readonly IDisposable _changeTokenRegistration;

        public PgSqlConfigurationProvider(PgSqlConfigurationSource source)
        {
            _source = source;

            _changeTokenRegistration = ChangeToken.OnChange(() => source.GetReloadToken(), Load);
        }

        public void Dispose()
        {
            _changeTokenRegistration?.Dispose();
        }

        public override void Load()
        {
            System.Console.WriteLine("[{0}][配置源更新]", DateTimeOffset.Now.ToLocalTime());

            using var dbContext = new InfrastructureDbContext(_source.DbOptions);
            dbContext.Database.EnsureCreated();
            
            var options = dbContext.ChannelOptions.AsNoTracking().ToList();
            options.ForEach(option =>
            {
                var data = option.JsonOptions.ToKeyValuePairs();
                var keyBuilder = new StringBuilder();
                foreach (var dataKv in data)
                {
                    keyBuilder.Clear();
                    var key = keyBuilder.Append(option.PostType).Append(':').Append(dataKv.Key).ToString();

                    // Data.Add(key, dataKv.Value);
                    Data[key] = dataKv.Value;
                }
            });
        }
    }
}