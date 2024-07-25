using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Infraestructure.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextClass _dbContextClass;

        public UserRepository(DbContextClass dbContextClass)
        {
            _dbContextClass = dbContextClass;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var result = _dbContextClass.Users.Add(user);

            await _dbContextClass.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            var filteredData = _dbContextClass.Users.Where(x => x.Id == id).FirstOrDefault();

            _dbContextClass.Users.Remove(filteredData);

            return await _dbContextClass.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContextClass.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContextClass.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUserListAsync()
        {
            return await _dbContextClass.Users.ToListAsync();
        }

        public async Task<int> UpdateUserAsync(User studentDetails)
        {
            _dbContextClass.Users.Update(studentDetails);
            return await _dbContextClass.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            return await _dbContextClass.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Password == password);
        }
    }
}
