using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10701
    {
        public int StatusHistoryID { get; set; }
        public int TRXTypeID { get; set; }
        public string CaseRef { get; set; }
        public int StatusActionTypeID { get; set; }
        public int ChangeStatusSourceID { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public double ClaimAmount { get; set; }
        public Nullable<double> OutstandingAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<int> LastCaseStatusID { get; set; }
        public Nullable<int> LastCasStatusChldID { get; set; }
        public string LastCasStatusname { get; set; }
        public string LastCasStatusChldNam { get; set; }
        public string LastCasStatusComment { get; set; }
        public Nullable<int> StatusScriptID { get; set; }
        public string AgentID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public virtual CM00027 CM00027 { get; set; }
        public virtual CM00203 CM00203 { get; set; }
    }
}
