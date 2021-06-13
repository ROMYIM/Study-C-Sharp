using ConfigurationDemo.Infranstructures.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConfigurationDemo.Infranstructures.Db
{
    public class InfrastructureDbContext : DbContext
    {

        public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options)
        {
            
        }

        public DbSet<ChannelOptions> ChannelOptions { get; set; }
    }
}