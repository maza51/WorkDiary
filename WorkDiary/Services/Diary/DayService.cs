using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkDiary.Data;
using WorkDiary.Entities;
using WorkDiary.Entities.Diary;

namespace WorkDiary.Services.Diary
{
    public class DayService : IDayService
    {
        private AppDbContext _dbContext;

        public DayService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Day>> GetAllAsync(string userName)
        {
            return await _dbContext.Days
                .Where(x => x.UserName == userName)
                .ToListAsync();
        }

        public async Task<Day> GetByIdAsync(int id, string userName)
        {
            return await _dbContext.Days
                .FirstOrDefaultAsync(x => x.Id == id && x.UserName == userName);
        }

        public async Task CreateOrUpdateAsync(Day day, string userName)
        {
            var dayInDb = await _dbContext.Days
                .FirstOrDefaultAsync(x => x.DayNumber == day.DayNumber && x.MonthId == day.MonthId && x.UserName == userName);

            if (dayInDb == null)
            {
                day.UserName = userName;

                await _dbContext.Days.AddAsync(day);
            }
            else
            {
                if (day.Note != null && day.Note.Length > 0)
                {
                    dayInDb.Note = day.Note;
                }

                if (day.WorkedHours != 0)
                {
                    dayInDb.WorkedHours = Math.Abs(Math.Min(day.WorkedHours, 24));
                }

                _dbContext.Days.Update(dayInDb);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Day day, string userName)
        {
            day.UserName = userName;

            await _dbContext.Days.AddAsync(day);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Day day)
        {
            _dbContext.Days.Remove(day);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Day day)
        {
            _dbContext.Days.Update(day);
            await _dbContext.SaveChangesAsync();
        }
    }
}

