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
using PosRi.Models.Request.User;
using PosRi.Models.Response;
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

        public async Task<bool> UserExists(int id)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == id && u.IsActive);
        }

        public async Task<bool> IsDuplicateUser(NewUserDto newUser)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.IsActive && u.Username.Equals(newUser.Username, StringComparison.InvariantCultureIgnoreCase));

        }

        public async Task<bool> IsDuplicateUser(EditUserDto user)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.IsActive && u.Id != user.Id && u.Username.Equals(user.Username, StringComparison.InvariantCultureIgnoreCase));
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

            var userRoles = new List<UserRole>();
            foreach (var role in newUser.Roles)
            {
                userRoles.Add(new UserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            user.Roles = userRoles;

            var userStores = new List<UserStore>();
            foreach (var store in newUser.Stores)
            {
                userStores.Add(new UserStore()
                {
                    StoreId = store.Id,
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

        public async Task<bool> EditUser(EditUserDto editUser)
        {
            var user = _dbContext.Users.Find(editUser.Id);

            user.Name = editUser.Name;
            user.Username = editUser.Username;
            user.Password = editUser.Password;
            user.Birthday = editUser.Birthday;
            user.HireDate = editUser.HireDate;

            user.Roles.Clear();
            var userRoles = new List<UserRole>();
            foreach (var role in editUser.Roles)
            {
                userRoles.Add(new UserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            user.Roles = userRoles;
            user.Stores.Clear();
            var userStores = new List<UserStore>();
            foreach (var store in editUser.Stores)
            {
                userStores.Add(new UserStore()
                {
                    StoreId = store.Id,
                    UserId = user.Id
                });
            }

            user.Stores = userStores;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            user.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
