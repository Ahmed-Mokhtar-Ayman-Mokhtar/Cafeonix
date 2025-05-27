using Cafeonix.Models;
using System.Data.Entity;

namespace Cafeonix.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=CafeonixDb"){}
        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
