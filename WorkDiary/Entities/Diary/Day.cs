using System;
namespace WorkDiary.Entities.Diary
{
    public class Day
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string Note { get; set; }
        public float WorkedHours { get; set; }
        public int MonthId { get; set; }

        public string UserName { get; set; }
    }
}

