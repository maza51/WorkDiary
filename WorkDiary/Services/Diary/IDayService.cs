using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkDiary.Entities.Diary;

namespace WorkDiary.Services.Diary
{
    public interface IDayService
    {
        Task CreateOrUpdateAsync(Day day, string userName);
        Task CreateAsync(Day day, string userName);
        Task DeleteAsync(Day day);
        Task<List<Day>> GetAllAsync(string userName);
        Task<Day> GetByIdAsync(int id, string userName);
        Task UpdateAsync(Day day);
    }
}