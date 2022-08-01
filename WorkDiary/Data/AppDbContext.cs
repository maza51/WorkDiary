using System;
using Microsoft.EntityFrameworkCore;
using WorkDiary.Entities;
using WorkDiary.Entities.Diary;

namespace WorkDiary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Month> Months { get; set; }
    }
}

