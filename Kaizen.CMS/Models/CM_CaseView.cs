using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_CaseView
    {
        public string CaseStatusname { get; set; }
        public string ChildCaseStatusName { get; set; }
        public string CaseRef { get; set; }
        public string ContractID { get; set; }
        public string TrxTypeName { get; set; }
        public string ClientID { get; set; }
        public string CurrencyCode { get; set; }
        public byte DecimalDigit { get; set; }
        public string DebtorID { get; set; }
        public Nullable<double> TaskProgress { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public string AddressCode { get; set; }
        public string CaseAddess { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public string CaseAccountNo { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string Remarks { get; set; }
        public double OSAmount { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public double FinanceCharge { get; set; }
        public double ClaimAmount { get; set; }
        public double PrincipleAmount { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string AgentID { get; set; }
        public System.DateTime AssignDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDate { get; set; }
        public Nullable<double> LastCallactedAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public string LastPaymentBy { get; set; }
    }
}
