using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.DBConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Roles> roles { get; set; }
    }
}
