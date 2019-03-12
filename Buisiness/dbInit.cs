using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buisiness;
using Microsoft.AspNetCore.Identity;


namespace DataAccess
{
    public class Dbinit
    {

        public static async Task InitializeAsync(ShopContext context,
                                      UserManager<Customer> userManager,
                                      RoleManager<ShopRole> roleManager)
        {
            context.Database.EnsureCreated();

            string role1 = "Admin";
            string description1 = "this is an admin";

            string role2 = "User";
            string description2 = "this is a user";

            await roleManager.CreateAsync(new ShopRole(role1, description1, DateTime.Now));
            await roleManager.CreateAsync(new ShopRole(role2, description2, DateTime.Now));

            string password = "123123";

            Customer c1 = new Customer
            {
                FirstName = "Vincent",
                Name = "Cremers",
                UserName = "vincent@cremers.be",
                Email = "vincent@cremers.be"
            };
            Customer c2 = new Customer
            {
                FirstName = "Nadine",
                Name = "Ringoot",
                UserName = "nadine@ringoot.be",
                Email = "nadine@ringoot.be"
            };
            Customer c3 = new Customer
            {
                FirstName = "Bill",
                Name = "Gates",
                UserName = "bill@gates.be",
                Email = "bill@gates.be"
            };
            Customer c4 = new Customer
            {
                FirstName = "Steve",
                Name = "Jobs",
                UserName = "Steve@Jobs.be",
                Email = "Steve@Jobs.be"
            };

            if (!context.Customers.Any())
            {

                var result = await userManager.CreateAsync(c1, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(c1, role1);
                }

                result = await userManager.CreateAsync(c2, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(c2, role2);
                }

                result = await (userManager.CreateAsync(c3, password));
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(c3, role2);
                }

                result = await (userManager.CreateAsync(c4, password));
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(c4, role2);
                }


                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product { Name = "Balpen", Price = 2.5 },
                        new Product { Name = "Boekentas", Price = 20 },
                        new Product { Name = "Pennezak", Price = 4.75 },
                        new Product { Name = "Passer", Price = 6.5 },
                        new Product { Name = "Meetlat", Price = 2.5 },
                        new Product { Name = "Gom", Price = 0.6 },
                        new Product { Name = "Rekenmachine", Price = 14 },
                        new Product { Name = "Kaft", Price = 1.2 },
                        new Product { Name = "Marker", Price = 1.2 },
                        new Product { Name = "Potlood", Price = 0.8 },
                        new Product { Name = "Geodriehoek", Price = 2.2 }
                        );

                }
                context.SaveChanges();
            }

            if (!context.ShoppingBags.Any())
            {
                ShoppingBagData sbd = new ShoppingBagData(context);
                for (int i = 0; i < 20; i++)
                {
                    sbd.AddRandomData();
                }

                context.SaveChanges();

            }

            if (!context.Customers.Where(c=> c.UserName=="admin@admin.be").Any())
            {
                Customer c = new Customer
                {
                    FirstName = "Admin",
                    Name = "",
                    UserName = "admin@admin.be",
                    Email = "admin@admin.be"
                };

                var result = await userManager.CreateAsync(c, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(c, role1);
                }

            }


        }

    }
}

