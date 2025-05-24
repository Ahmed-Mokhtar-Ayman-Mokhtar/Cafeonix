using Cafeonix.Models;
using Cafeonix.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeonix.Functions
{
    public class UserFunctions
    {
        private readonly AppDbContext context;

        public UserFunctions(AppDbContext context)
        {
            this.context = context;
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users.Find(id);
        }

        public void add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void update(User user)
        {
            context.Users.AddOrUpdate(user);
            context.SaveChanges();
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
