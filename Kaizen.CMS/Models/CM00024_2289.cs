using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00024_2289
    {
        public int StatusHistoryID { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public Nullable<int> StatusScriptID { get; set; }
        public string CaseRef { get; set; }
        public double ClaimAmount { get; set; }
        public string AgentID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ChangeStatusSourceID { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public string Lookup01 { get; set; }
        public string Lookup01ValueName { get; set; }
        public string Lookup02 { get; set; }
        public string Lookup02ValueName { get; set; }
        public Nullable<System.DateTime> Date01 { get; set; }
        public Nullable<System.DateTime> Date02 { get; set; }
    }
}
