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
    public class PgSqlConfigurationProvider : ConfigurationProvider
    {
        private readonly PgSqlConfigurationSource _source;

        public PgSqlConfigurationProvider(PgSqlConfigurationSource source)
        {
            _source = source;

            
        }

        public override void Load()
        {
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

                    Data.Add(key, dataKv.Value);
                }
            });
        }
    }
}