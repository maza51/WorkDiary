using System;
using System.Collections.Generic;

namespace WorkDiary.Entities.Diary
{
    public class Month
    {
        public int Id { get; set; }
        public int YearNumber { get; set; }
        public int MonthNumber { get; set; }
        public string Note { get; set; }
        public List<Day> Days { get; set; }

        public string UserName { get; set; }
    }
}

