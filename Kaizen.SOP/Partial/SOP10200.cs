using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.SOP
{
    public partial class SOP10200
    {
        [NotMapped]
        public string QuotationAge
        {
            get
            {
                int ageInYears = 0;
                int ageInMonths = 0;
                int ageInDays = 0;
                CalculateAge(this.DOCDATE, out ageInYears, out ageInMonths, out ageInDays);
                return ageInYears.ToString() + " Year(s) - " + ageInMonths.ToString() + " Month(s) - " + ageInDays.ToString() + " Day(s) Ago";
            }
        }
        private static void CalculateAge(DateTime DocDate, out int Year, out int Month, out int Day)
        {
            // get current date.
            DateTime adtCurrentDate = DateTime.Now;

            // find the literal difference
            Day = adtCurrentDate.Day - DocDate.Day;
            Month = adtCurrentDate.Month - DocDate.Month;
            Year = adtCurrentDate.Year - DocDate.Year;

            if (Day < 0)
            {
                Day += DateTime.DaysInMonth(adtCurrentDate.Year, adtCurrentDate.Month);
                Month--;
            }

            if (Month < 0)
            {
                Month += 12;
                Year--;
            }
        }

    }
}
