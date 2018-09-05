using CoolBooks.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CoolBooks.Migrations
{
    public static class SeedData { 
    public static void SeedUsers(CoolBooks.Models.ApplicationDbContext context)
    {
        var passwordHash = new PasswordHasher();

            context.Roles.AddOrUpdate(
            new IdentityRole { Id = "1", Name = "Admin" },
            new IdentityRole { Id = "2", Name = "BasicUser" }
            );

            context.Users.AddOrUpdate(u => u.UserName,
            new ApplicationUser
            {
                UserName = "Test1@test.se",
                PasswordHash = passwordHash.HashPassword("Test1@test.se"),
                PhoneNumber = "12345678911",
                Email = "Test1@test.se",
                SecurityStamp = Guid.NewGuid().ToString("D")

            },
            new ApplicationUser
            {
                UserName = "Test2@test.se",
                PasswordHash = passwordHash.HashPassword("Test2@test.se"),
                PhoneNumber = "12345678911",
                Email = "Test2@test.se",
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new ApplicationUser
            {
                UserName = "Test3@test.se",
                PasswordHash = passwordHash.HashPassword("Test3@test.se"),
                PhoneNumber = "12345678911",
                Email = "Test3@test.se",
                SecurityStamp = Guid.NewGuid().ToString("D")
            }
            ,
            new ApplicationUser
            {
                UserName = "TestAdmin1@test.se",
                PasswordHash = passwordHash.HashPassword("TestAdmin1@test.se"),
                PhoneNumber = "12345678911",
                Email = "TestAdmin1@test.se",
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new ApplicationUser
            {
                UserName = "TestAdmin2@test.se",
                PasswordHash = passwordHash.HashPassword("TestAdmin2@test.se"),
                PhoneNumber = "12345678911",
                Email = "TestAdmin2@test.se",
                SecurityStamp = Guid.NewGuid().ToString("D")
            }
            );

            context.SaveChanges();

        }

    }
}