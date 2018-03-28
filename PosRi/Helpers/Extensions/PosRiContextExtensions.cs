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

            if (!await context.States.AnyAsync())
            {
                List<State> states = new List<State>
                {
                    new State {Name = "Aguascalientes", ShortName = "AGS"},
                    new State {Name = "Baja California", ShortName = "BC"},
                    new State {Name = "Baja California Sur", ShortName = "BCS"},
                    new State {Name = "Campeche", ShortName = "CAMP"},
                    new State {Name = "Chiapas", ShortName = "CHIS"},
                    new State {Name = "Chihuahua", ShortName = "CHIH"},
                    new State {Name = "Ciudad de México", ShortName = "CDMX"},
                    new State {Name = "Coahuila de Zaragoza", ShortName = "COAH"},
                    new State {Name = "Colima", ShortName = "COL"},
                    new State {Name = "Durango", ShortName = "DGO"},
                    new State {Name = "Guanajuato", ShortName = "GTO"},
                    new State {Name = "Guerrero", ShortName = "GRO"},
                    new State {Name = "Hidalgo", ShortName = "HGO"},
                    new State {Name = "Jalisco", ShortName = "JAL"},
                    new State {Name = "México", ShortName = "MEX"},
                    new State {Name = "Michoacán de Ocampo", ShortName = "MICH"},
                    new State {Name = "Morelos", ShortName = "MOR"},
                    new State {Name = "Nayarit", ShortName = "NAY"},
                    new State {Name = "Nuevo León", ShortName = "NL"},
                    new State {Name = "Oaxaca", ShortName = "OAX"},
                    new State {Name = "Puebla", ShortName = "PUE"},
                    new State {Name = "Querétaro de Arteaga", ShortName = "QRO"},
                    new State {Name = "Quintana Roo", ShortName = "QR"},
                    new State {Name = "San Luis Potosí", ShortName = "SLP"},
                    new State {Name = "Sinaloa", ShortName = "SL"},
                    new State {Name = "Sonora", ShortName = "SON"},
                    new State {Name = "Tabasco", ShortName = "TAB"},
                    new State {Name = "Tamaulipas", ShortName = "TAMPS"},
                    new State {Name = "Tlaxcala", ShortName = "TLAX"},
                    new State {Name = "Veracruz de Ignacio de la Llave", ShortName = "VER"},
                    new State {Name = "Yucatán", ShortName = "YUC"},
                    new State {Name = "Zacatecas", ShortName = "ZAC"}
                };

                foreach (var state in states)
                {                   
                    await context.States.AddAsync(state);
                    await context.SaveChangesAsync();

                }
            }

            await context.SaveChangesAsync();
        }
    }
}
