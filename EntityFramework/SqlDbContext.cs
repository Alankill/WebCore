using Core.Domain.Tasks;
using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Web.EntityFramework
{
    public class SqlDbContext:DbContext
    {
        public SqlDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(m => m.Name).IsUnique();
        }
    }
}
