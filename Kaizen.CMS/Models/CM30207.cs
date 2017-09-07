using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM30207
    {
        public CM30207()
        {
            this.CM30204 = new List<CM30204>();
        }

        public string PaymentID { get; set; }
        public int PERIODID { get; set; }
        public string YearCode { get; set; }
        public string PeriodName { get; set; }
        public string DebtorID { get; set; }
        public string CheckbookCode { get; set; }
        public string CheckbookName { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsMultiply { get; set; }
        public double ExchangeRate { get; set; }
        public byte DecimalDigit { get; set; }
        public double Amount { get; set; }
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
        public virtual ICollection<CM30204> CM30204 { get; set; }
    }
}
