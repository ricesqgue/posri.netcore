using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Helper;
using PosRi.Models.Request;
using PosRi.Services.Contracts;

namespace PosRi.Services
{
    public class UserService : IUserService
    {
        private readonly PosRiContext _dbContext;

        public UserService(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserWithManyToManyRelation>> GetUsers()
        {
           return await _dbContext.Users
               .Where(u => u.IsActive)
               .OrderBy(u => u.Name)
               .Select(user => new UserWithManyToManyRelation()
               {
                   Id = user.Id,
                   Birthday = user.Birthday,
                   HireDate = user.HireDate,
                   Name = user.Name,
                   Username = user.Username,
                   Password = user.Password,
                   Roles = user.Roles.Select(role => role.Role).ToList(),
                   Stores = user.Stores.Select(store => store.Store).ToList()                  
               })
               .ToListAsync();
        }

        public async Task<UserWithManyToManyRelation> GetUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);
            if (user != null)
            {
                return new UserWithManyToManyRelation()
                {
                    Id = user.Id,
                    Birthday = user.Birthday,
                    HireDate = user.HireDate,
                    Name = user.Name,
                    Username = user.Username,
                    Password = user.Password,
                    Roles = user.Roles.Select(role => role.Role).ToList(),
                    Stores = user.Stores.Select(store => store.Store).ToList()
                };
            }

            return null;
        }

        public async Task<User> Authenticate(LoginDto login)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(login.Username) && user.Password.Equals(login.Password) && user.IsActive);
        }

        public async Task<bool> UserExists(NewUserDto newUser)
        {
            return await _dbContext.Users
                .AnyAsync(user => user.Username.Equals(newUser.Username, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<int> AddUser(NewUserDto newUser)
        {
            var user = new User
            {
                Name = newUser.Name,
                Username = newUser.Username,
                Password = newUser.Password,
                Birthday = newUser.Birthday,
                HireDate = newUser.HireDate,
                IsActive = true
            };
            //await _dbContext.SaveChangesAsync();

            var userRoles = new List<UserRole>();
            foreach (var role in newUser.Roles)
            {
                userRoles.Add(new UserRole
                {
                    RoleId = role,
                    UserId = user.Id
                });
            }

            user.Roles = userRoles;

            var userStores = new List<UserStore>();
            foreach (var store in newUser.Stores)
            {
                userStores.Add(new UserStore()
                {
                    StoreId = store,
                    UserId = user.Id
                });
            }

            user.Stores = userStores;

            await _dbContext.Users.AddAsync(user);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return user.Id;
            }

            return 0;

        }
    }
}
