using FinaControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Data
{
    public class FinaControlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}