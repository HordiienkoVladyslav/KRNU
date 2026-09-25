using System;

namespace LB9_1
{
    public interface IBirthdayService
    {
        DayOfWeek GetBirthDayOfWeek(Birthday b);

        DayOfWeek GetDayOfWeekInYear(Birthday b, int targetYear);
        int GetDaysUntilNextBirthday(Birthday b);
    }
}