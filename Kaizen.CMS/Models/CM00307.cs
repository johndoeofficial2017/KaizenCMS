using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00307
    {
        public CM00307()
        {
            this.CM00308 = new List<CM00308>();
        }

        public string PaymentID { get; set; }
        public string CheckbookCode { get; set; }
        public string CurrencyCode { get; set; }
        public string ExchangeTableID { get; set; }
        public bool IsMultiply { get; set; }
        public double ExchangeRate { get; set; }
        public byte DecimalDigit { get; set; }
        public double Amount { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string TransDescription { get; set; }
        public int PaymentMethodID { get; set; }
        public string AgentID { get; set; }
        public int ReasonID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string PaymentNumber { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public Nullable<System.DateTime> BankCheckDate { get; set; }
        public virtual CM00309 CM00309 { get; set; }
        public virtual ICollection<CM00308> CM00308 { get; set; }
    }
}
