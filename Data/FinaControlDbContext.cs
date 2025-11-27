using FinaControl.Data.Mappings;
using FinaControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Data
{
    public class FinaControlDbContext(DbContextOptions<FinaControlDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
        }
    }
}