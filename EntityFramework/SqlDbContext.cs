using Core.Domain.Tasks;
using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Web.EntityFramework
{
    public class SqlDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WebCore;Persist Security Info=True;User ID=sa;Password=123456;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(m => m.Name).IsUnique();
        }
    }
}
