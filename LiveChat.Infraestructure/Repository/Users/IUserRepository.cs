using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Infraestructure.Repository.Users
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUserListAsync();
        public Task<User> GetUserByIdAsync(int Id);
        public Task<User> AddUserAsync(User user);
        public Task<int> UpdateUserAsync(User user);
        public Task<int> DeleteUserAsync(int Id);
    }
}
