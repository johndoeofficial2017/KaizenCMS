using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM30205
    {
        public int StatusHistoryID { get; set; }
        public int TRXTypeID { get; set; }
        public int StatusActionTypeID { get; set; }
        public int ChangeStatusSourceID { get; set; }
        public string CaseRef { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
    }
}
