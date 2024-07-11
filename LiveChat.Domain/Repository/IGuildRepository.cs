using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Repository
{
    public interface IGuildRepository
    {
        public Task<List<Guild>> GetAvailableGuilds();
        public Task<User> GetUserByIdAsync(int Id);
        public Task<User> AddUserAsync(User user);
        public Task<int> UpdateUserAsync(User user);
        public Task<int> DeleteUserAsync(int Id);
    }
}
