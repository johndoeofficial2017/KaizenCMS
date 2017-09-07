using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00600
    {
        public SOP00600()
        {
            this.SOP00601 = new List<SOP00601>();
        }

        public int KendoID00600 { get; set; }
        public short PERIODID { get; set; }
        public int YearCode { get; set; }
        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public byte SOPTYPE { get; set; }
        public int TransactionID { get; set; }
        public Nullable<int> MasterID { get; set; }
        public string BatchID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public decimal DOCAMNT { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public string CustomerPoNumber { get; set; }
        public decimal AccountsReceivableAMT { get; set; }
        public string ShipTo { get; set; }
        public System.DateTime ShipDate { get; set; }
        public string SiteID { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<decimal> Markdown { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public Nullable<decimal> Miscellaneous { get; set; }
        public Nullable<decimal> TradeDiscount { get; set; }
        public string TrxComments { get; set; }
        public string ExchangeTableID { get; set; }
        public int ExchangeRateID { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public string SalesPersonID { get; set; }
        public System.DateTime EntryDateOriginal { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string UserCode { get; set; }
        public System.DateTime PostingDate { get; set; }
        public string PostingUserCode { get; set; }
        public int GL00200ID { get; set; }
        public virtual ICollection<SOP00601> SOP00601 { get; set; }
    }
}
