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

        for (DateTime i = startDate.Date; i <= endDate.Date; i.Date.AddDays(1))
        {
            if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
            {
                totalDaysVacation = totalDaysVacation--;
            }
        }

        return totalDaysVacation;
    }
}
