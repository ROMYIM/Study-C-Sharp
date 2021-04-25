using Microsoft.EntityFrameworkCore;
using NettyDemo.Infrastructure.Db.Models;

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