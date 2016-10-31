using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public static class DateTimeExtensie
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int dag = (int)dt.DayOfWeek;
            int start = (int)startOfWeek;
            int diff = dag + start;
            int vers = 0;
            switch (diff)
            {
                case 7: { vers = 9; break; }
                case 6: { vers = 10; break; }
                case 5: { vers = 4; break; }
                case 4: { vers = 5; break; }
                case 3: { vers = 6; break; }
                case 2: { vers = 7; break; }
                case 1: { vers = 8; break; }
                default: break;
            }
            if (dt.DayOfWeek == DayOfWeek.Friday && dt.Hour < 17)
                vers = 3;
            return dt.AddDays(vers).Date;
        }
    }
}