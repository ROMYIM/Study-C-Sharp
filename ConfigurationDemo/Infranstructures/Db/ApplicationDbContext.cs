using Microsoft.EntityFrameworkCore;

namespace ConfigurationDemo.Infranstructures.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}