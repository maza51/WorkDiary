using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkDiary.Data;
using WorkDiary.Entities.Diary;

namespace WorkDiary.Services.Diary
{
    public class MonthService : IMonthService
    {
        private AppDbContext _dbContext;

        public MonthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Month>> GetAllAsync(string userName)
        {
            return await _dbContext.Months
                .Where(x => x.UserName == userName)
                .Include(x => x.Days)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Month> GetByIdAsync(int id, string userName)
        {
            return await _dbContext.Months
                .Where(x => x.UserName == userName)
                .Include(x => x.Days)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateCurrentMonthIfDoesNotExistAsync(string userName)
        {
            var month = await _dbContext.Months
                .FirstOrDefaultAsync(x => x.YearNumber == DateTime.Now.Year
                                          && x.MonthNumber == DateTime.Now.Month
                                          && x.UserName == userName);

            if (month == null)
            {
                await _dbContext.Months.AddAsync(new Month
                {
                    YearNumber = DateTime.Now.Year,
                    MonthNumber = DateTime.Now.Month,
                    UserName = userName
                });

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(Month month, string userName)
        {
            month.UserName = userName;

            await _dbContext.Months.AddAsync(month);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Month month)
        {
            _dbContext.Months.Remove(month);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Month month)
        {
            _dbContext.Months.Update(month);
            await _dbContext.SaveChangesAsync();
        }
    }
}

