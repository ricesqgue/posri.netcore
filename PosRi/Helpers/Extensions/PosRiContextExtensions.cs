using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;

namespace PosRi.Helpers.Extensions
{
    public static class PosRiContextExtensions
    {
        public static async void EnsureSeedDataForContext(this PosRiContext context)
        {
            
            if (!await context.Roles.AnyAsync())
            {
                context.Roles.SeedEnumValues<Role, Role.RoleNames>(@enum => @enum);
            }


            if (!await context.Users.AnyAsync())
            {
                var user = new User()
                {
                    Name = "Ricardo Esqueda Guerrero",
                    Username = "ricesqgue",
                    Birthday = new DateTime(1994, 6, 6),
                    HireDate = DateTime.Now,
                    Password = "root"
                };


                await context.Users.AddAsync(user);

                var userRole = new UserRole
                {
                    RoleId = (int)Role.RoleNames.SuperAdmin,
                    UserId = user.Id
                };

                user.Roles = new List<UserRole> {userRole};
            }

            await context.SaveChangesAsync();
        }
    }
}
