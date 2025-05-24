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
    public class MenuItemFunctions
    {
        private readonly AppDbContext context;

        public MenuItemFunctions(AppDbContext context)
        {
            this.context = context;
        }

        public List<MenuItem> GetAll()
        {
            return context.MenuItems.ToList();
        }

        public MenuItem GetById(int id)
        {
            return context.MenuItems.Find(id);
        }

        public void add(MenuItem menuItem)
        {
            context.MenuItems.Add(menuItem);
            context.SaveChanges();
        }

        public void update(MenuItem menuItem)
        {
            context.MenuItems.AddOrUpdate(menuItem);
            context.SaveChanges();
        }

        public void Delete(MenuItem menuItem)
        {
            context.MenuItems.Remove(menuItem);
            context.SaveChanges();
        }
    }
}
