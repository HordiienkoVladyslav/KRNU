using System;

namespace LB9_1
{
    public class BirthdayService : IBirthdayService
    {
        public DayOfWeek GetBirthDayOfWeek(Birthday b)
        {
            return b.Date.DayOfWeek;
        }

        public DayOfWeek GetDayOfWeekInYear(Birthday b, int targetYear)
        {
            DateTime targetDate = new DateTime(targetYear, b.Date.Month, b.Date.Day);
            return targetDate.DayOfWeek;
        }

        public int GetDaysUntilNextBirthday(Birthday b)
        {
            DateTime today = DateTime.Today;
            DateTime nextBirthday = new DateTime(today.Year, b.Date.Month, b.Date.Day);

            if (nextBirthday < today)
            {
                nextBirthday = nextBirthday.AddYears(1);
            }

            return (nextBirthday - today).Days;
        }
    }
}