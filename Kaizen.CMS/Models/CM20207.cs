using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM20207
    {
        public string PaymentID { get; set; }
        public string YearCode { get; set; }
        public int PERIODID { get; set; }
        public string PeriodName { get; set; }
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public string CheckbookCode { get; set; }
        public string CheckbookName { get; set; }
        public string CurrencyCode { get; set; }
        public byte DecimalDigit { get; set; }
        public double PaymentAmount { get; set; }
        public double PaymentAmountF { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string TrxDescription { get; set; }
        public int PaymentMethodID { get; set; }
        public string AgentID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string PostingBy { get; set; }
        public System.DateTime PostingDate { get; set; }
        public string PaymentNumber { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public Nullable<System.DateTime> BankCheckDate { get; set; }
        public bool IsApproved { get; set; }
        public string VoidBy { get; set; }
        public Nullable<System.DateTime> VoidDate { get; set; }
        public Nullable<System.DateTime> VoidSystemDate { get; set; }
    }
}
