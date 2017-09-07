using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10300
    {
        public SOP10300()
        {
            this.SOP10301 = new List<SOP10301>();
            this.SOP10302 = new List<SOP10302>();
            this.SOP10303 = new List<SOP10303>();
            this.SOP10304 = new List<SOP10304>();
        }

        public string SOPNUMBE { get; set; }
        public string SiteID { get; set; }
        public string SubBinID { get; set; }
        public Nullable<int> BinID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public string BatchID { get; set; }
        public string CheckbookCode { get; set; }
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
        public string InsuranceID { get; set; }
        public Nullable<double> InsuranceAMT { get; set; }
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
        public int KaizenID { get; set; }
        public virtual ICollection<SOP10301> SOP10301 { get; set; }
        public virtual ICollection<SOP10302> SOP10302 { get; set; }
        public virtual ICollection<SOP10303> SOP10303 { get; set; }
        public virtual ICollection<SOP10304> SOP10304 { get; set; }
    }
}
