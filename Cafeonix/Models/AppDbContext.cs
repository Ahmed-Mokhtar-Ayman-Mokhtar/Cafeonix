using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeonix.Models
{
    class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=CafeonixDb")
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
