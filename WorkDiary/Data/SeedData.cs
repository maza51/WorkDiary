using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkDiary.Entities;
using WorkDiary.Enums;

namespace WorkDiary.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider service)
        {

            using (var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var passwordHasher = new PasswordHasher<User>();

                var user = new User
                {
                    UserName = "Admin",
                    Password = passwordHasher.HashPassword(new User(), "Admin"),
                    Role = (int)Roles.Admin
                };

                context.Users.Add(user);

                context.SaveChanges();
            }

        }
    }
}

