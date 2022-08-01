using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkDiary.Entities;

namespace WorkDiary.Services
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUserNameAsync(string userName);
        Task UpdateAsync(User user);
    }
}