using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00007
    {
        public CM00007()
        {
            this.CM00120 = new List<CM00120>();
        }

        public int PERIODID { get; set; }
        public int CalendarID { get; set; }
        public string YearCode { get; set; }
        public string PeriodName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }
        public Nullable<double> TargetAmount { get; set; }
        public Nullable<double> OutStandingAMT { get; set; }
        public Nullable<double> CollectedAmount { get; set; }
        public Nullable<double> DebtorCount { get; set; }
        public Nullable<double> CaseCount { get; set; }
        public virtual CM00005 CM00005 { get; set; }
        public virtual ICollection<CM00120> CM00120 { get; set; }
    }
}
