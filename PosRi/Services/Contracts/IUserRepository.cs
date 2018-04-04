using PosRi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Models.Helper;
using PosRi.Models.Request;
using PosRi.Models.Request.User;
using PosRi.Models.Response;

namespace PosRi.Services.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserWithManyToManyRelation>> GetUsersAsync();

        Task<UserWithManyToManyRelation> GetUserAsync(int userId);

        Task<User> AuthenticateAsync(LoginDto login);

        Task<bool> UserExistsAsync(int id);
            
        Task<bool> IsDuplicateUserAsync(NewUserDto newUser);

        Task<bool> IsDuplicateUserAsync(EditUserDto editUser);

        Task<int> AddUserAsync(NewUserDto newUser);

        Task<bool> EditUserAsync(EditUserDto editUser);

        Task<bool> DeleteUserAsync(int id);
    }
}
