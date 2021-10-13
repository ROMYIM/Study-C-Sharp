using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Db
{
    public class ConfigurationContext : DbContext
    {
        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {
            
        }

        public DbSet<ClientIdentityInfo> ClientIdentityInfoes { get; set; }
    }
}