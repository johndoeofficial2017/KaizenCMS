using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10310
    {
        public SOP10310()
        {
            this.SOP10311 = new List<SOP10311>();
        }

        public string TrxNumber { get; set; }
        public string SOPNUMBE { get; set; }
        public double DOCAMNT { get; set; }
        public string CurrencyCode { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public byte DecimalDigit { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public string Pnone01 { get; set; }
        public string Pnone02 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string POBox { get; set; }
        public string Other01 { get; set; }
        public string Other02 { get; set; }
        public string Address1 { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
        public virtual ICollection<SOP10311> SOP10311 { get; set; }
    }
}
