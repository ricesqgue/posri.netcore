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
        Task<IEnumerable<UserWithManyToManyRelation>> GetUsers();

        Task<UserWithManyToManyRelation> GetUser(int userId);

        Task<User> Authenticate(LoginDto login);

        Task<bool> UserExists(int id);
            
        Task<bool> IsDuplicateUser(NewUserDto newUser);

        Task<bool> IsDuplicateUser(EditUserDto editUser);

        Task<int> AddUser(NewUserDto newUser);

        Task<bool> EditUser(EditUserDto editUser);

        Task<bool> DeleteUser(int id);
    }
}
