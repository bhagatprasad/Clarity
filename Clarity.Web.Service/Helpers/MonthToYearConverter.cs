using System.Globalization;

namespace Clarity.Web.Service.Helpers
{
    public class MonthToYearConverter
    {
        public static int GetDaysInMonth(string monthName, int year)
        {
            int monthNumber = GetMonthNumber(monthName);
            return DateTime.DaysInMonth(year, monthNumber);
        }
        private static int GetMonthNumber(string monthName)
        {
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;
            return dtfi.MonthNames.ToList().IndexOf(monthName) + 1;
        }
        public static int GetAdjustedMonthNumber(string monthName, int year)
        {
            int monthNumber = GetMonthNumber(monthName);
            int startMonth = 4; // April is month number 4
           
            if (monthNumber < startMonth)
            {
                year--;
            }

            return (monthNumber + 12 - startMonth) % 12 + 1;
        }
    }
}
