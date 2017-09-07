using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00207
    {
        public CM00207()
        {
            this.CM00204 = new List<CM00204>();
        }

        public string PaymentID { get; set; }
        public string DebtorID { get; set; }
        public string CheckbookCode { get; set; }
        public string CurrencyCode { get; set; }
        public string ExchangeTableID { get; set; }
        public bool IsMultiply { get; set; }
        public int ExchangeRateID { get; set; }
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
        public bool IsApproved { get; set; }
        public string VoidBy { get; set; }
        public Nullable<System.DateTime> VoidDate { get; set; }
        public Nullable<System.DateTime> VoidSystemDate { get; set; }
        public virtual ICollection<CM00204> CM00204 { get; set; }
    }
}
