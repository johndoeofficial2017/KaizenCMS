using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP20300
    {
        public SOP20300()
        {
            this.SOP20301 = new List<SOP20301>();
        }

        public string SOPNUMBE { get; set; }
        public string SiteID { get; set; }
        public string SubBinID { get; set; }
        public Nullable<int> BinID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public string CurrencyCode { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public byte DecimalDigit { get; set; }
        public double DOCAMNT { get; set; }
        public double CollectedAMT { get; set; }
        public double PartialAMT { get; set; }
        public double TotalCash { get; set; }
        public double TotalCheck { get; set; }
        public double TotalCreditCard { get; set; }
        public double TotalVoucher { get; set; }
        public Nullable<double> TaxAMT { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<decimal> Markdown { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
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
        public string TrxComments { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<SOP20301> SOP20301 { get; set; }
    }
}
