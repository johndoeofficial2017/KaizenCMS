using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00500
    {
        public SOP00500()
        {
            this.SOP00503 = new List<SOP00503>();
            this.SOP00504 = new List<SOP00504>();
            this.SOP00508 = new List<SOP00508>();
        }

        public string TrxDocumentID { get; set; }
        public string BatchID { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public Nullable<bool> ApplyToCustomer { get; set; }
        public string CurrencyCode { get; set; }
        public string ExchangeTableID { get; set; }
        public int ExchangeRateID { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public byte DecimalDigit { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public System.DateTime PostDate { get; set; }
        public double DOCAMNT { get; set; }
        public double DOCAMNTUnAply { get; set; }
        public int PaymentMethodID { get; set; }
        public string CheckbookCode { get; set; }
        public string CheckbookTrxID { get; set; }
        public string TrxDescription { get; set; }
        public virtual SOP00501 SOP00501 { get; set; }
        public virtual Sys00020 Sys00020 { get; set; }
        public virtual ICollection<SOP00503> SOP00503 { get; set; }
        public virtual ICollection<SOP00504> SOP00504 { get; set; }
        public virtual ICollection<SOP00508> SOP00508 { get; set; }
    }
}
