using Buisiness;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ShopContext:IdentityDbContext<Customer,ShopRole, string>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingItem> ShoppingItems  { get; set; }
        public DbSet<ShoppingBag> ShoppingBags { get; set; }


    }
}
