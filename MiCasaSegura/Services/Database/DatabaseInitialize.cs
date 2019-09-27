using System;
using System.Linq;
using MiCasaSegura.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace MiCasaSegura.Services.Database
{
    public class DatabaseInitialize
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                User user = new User();
                user.UserName = "admin";
                user.FirstName = "Brian Youssef";
                user.LastNames = "Jerez Baez";
                user.Email = "brianjerezbaez@gmail.com";

                var identityResult = userManager.CreateAsync(user, "123123").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (roleManager.Roles.ToList().Count == 0)
            {
                Role adminRole = new Role();
                adminRole.Name = Constants.Roles.Administrator;

                roleManager.CreateAsync(adminRole).Wait();

                Role customerRole = new Role();
                customerRole.Name = Constants.Roles.Customer;

                roleManager.CreateAsync(customerRole).Wait();

                Role warehouseRole = new Role();
                warehouseRole.Name = Constants.Roles.Warehouse;

                roleManager.CreateAsync(warehouseRole).Wait();

                Role salesRole = new Role();
                salesRole.Name = Constants.Roles.SalesRep;

                roleManager.CreateAsync(salesRole).Wait();
            }
        }
    }
}
