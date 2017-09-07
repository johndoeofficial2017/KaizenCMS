using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00205View01
    {
        public Nullable<int> StatusYear { get; set; }
        public string StatusMonth { get; set; }
        public Nullable<int> StatusDay { get; set; }
        public int CaseStatusID { get; set; }
        public string CaseStatusname { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusChildName { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public Nullable<int> StatusHistoryID { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public Nullable<double> OutstandingAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
    }
}
