using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkDiary.Entities.Diary;

namespace WorkDiary.Services.Diary
{
    public interface IMonthService
    {
        Task CreateCurrentMonthIfDoesNotExistAsync(string userName);
        Task CreateAsync(Month month, string userName);
        Task DeleteAsync(Month month);
        Task<List<Month>> GetAllAsync(string userName);
        Task<Month> GetByIdAsync(int id, string userName);
        Task UpdateAsync(Month month);
    }
}