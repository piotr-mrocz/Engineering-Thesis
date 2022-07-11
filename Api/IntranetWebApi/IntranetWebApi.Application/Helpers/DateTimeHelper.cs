using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Consts;
using IntranetWebApi.Domain.Models.ViewModels;

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

    public static (decimal workHours, decimal extraWorkHours) GetWorkHours(TimeSpan startTime, TimeSpan endTime)
    {
        var totalHours = (endTime - startTime).TotalHours;

        if (totalHours <= WorkTimeConst.OfficialWorkHoursPerDay)
            return ((decimal)totalHours, 0m);

        var hourHours = WorkTimeConst.OfficialWorkHoursPerDay;
        var extraHours = totalHours - hourHours;

        return ((decimal)hourHours, (decimal)extraHours);
    }

    public static List<FreeDaysListVM> GetFreeDays(int year)
    {
        var easterSunday = GetEasterSunday(year);
        var easterMonday = easterSunday.AddDays(1);
        var corpusChristi = easterMonday.AddDays(60);

        var days = new List<FreeDaysListVM>()
        {
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 1, 1),
                FreeDayName = "Nowy rok"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 1, 6),
                FreeDayName = "Święto Trzech Króli"
            },
            new FreeDaysListVM()
            {
                FreeDay = easterSunday,
                FreeDayName = "Niedziela Wielkanocna"
            },
            new FreeDaysListVM()
            {
                FreeDay = easterMonday,
                FreeDayName = "Poniedziałek Wielkanocny"
            },
            new FreeDaysListVM()
            {
                FreeDay = corpusChristi,
                FreeDayName = "Boże Ciało"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 8, 15),
                FreeDayName = "Święto Wojska Polskiego"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 11, 1),
                FreeDayName = "Wszystkich Świętych"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 11, 11),
                FreeDayName = "Święto Niepodległości"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 12, 24), // because our company decides that this day is free
                FreeDayName = "Wigilia Bożego Narodzenia"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 12, 25),
                FreeDayName = "Boże Narodzenie"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 12, 26),
                FreeDayName = "Drugi Dzień Świąt"
            },
            new FreeDaysListVM()
            {
                FreeDay = new DateTime(year, 12, 31), // because our company decides that this day is free
                FreeDayName = "Sylwester"
            }
        };

        return days;
    }

    private static DateTime GetEasterSunday(int year)
    {
        int g = year % 19;
        int c = year / 100;
        int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

        var day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
        var month = 3;

        if (day > 31)
        {
            month++;
            day -= 31;
        }

        return new DateTime(year, month, day);
    }
}


