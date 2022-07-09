using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Helpers;

public static class DateTimeHelper
{
    public static int CalculateTotalDaysBetweenDatesWithoutWeekends(DateTime startDate, DateTime endDate)
    {
        var totalDaysVacation = (int)(endDate.Date - startDate.Date).TotalDays + 1;

        for (var day = startDate.Date; day <= endDate.Date; day = day.Date.AddDays(1))
        {
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
            {
                totalDaysVacation = totalDaysVacation - 1;
            }
        }

        return totalDaysVacation;
    }
}
