using Microsoft.EntityFrameworkCore;
using NettyDemo.Models;

namespace NettyDemo.Infrastructure.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; }
    }
}