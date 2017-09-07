using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10100
    {
        public CM10100()
        {
            this.CM00203 = new List<CM00203>();
        }

        public string BatchID { get; set; }
        public string ClientID { get; set; }
        public string ContractID { get; set; }
        public string ClientName { get; set; }
        public string ContractName { get; set; }
        public string CreditorID { get; set; }
        public string CreditorName { get; set; }
        public string YearCode { get; set; }
        public Nullable<int> PERIODID { get; set; }
        public string PeriodName { get; set; }
        public System.DateTime BookingDate { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UploadedFileName { get; set; }
        public string CurrencyCode { get; set; }
        public byte DecimalDigit { get; set; }
        public bool IsMultiply { get; set; }
        public double ExchangeRate { get; set; }
        public virtual CM00200 CM00200 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
