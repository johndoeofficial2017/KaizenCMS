using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10207
    {
        public string PaymentID { get; set; }
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public string CheckbookCode { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsOneSerialNumberSale { get; set; }
        public string ExchangeTableID { get; set; }
        public bool IsMultiply { get; set; }
        public int ExchangeRateID { get; set; }
        public double ExchangeRate { get; set; }
        public byte DecimalDigit { get; set; }
        public double PaymentAmount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public int PaymentMethodID { get; set; }
        public string AgentID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string PaymentNumber { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public Nullable<System.DateTime> BankCheckDate { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public virtual CM00203 CM00203 { get; set; }
    }
}
