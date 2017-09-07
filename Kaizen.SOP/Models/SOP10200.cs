using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10200
    {
        public SOP10200()
        {
            this.SOP10201 = new List<SOP10201>();
        }

        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public string TransactionID { get; set; }
        public string PaymentTermID { get; set; }
        public string BatchID { get; set; }
        public string ProjectID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public Nullable<System.DateTime> QuoteExpirationDate { get; set; }
        public double DOCAMNT { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public string ShipAddressTypeCode { get; set; }
        public string ShipAddressName { get; set; }
        public string WebSite { get; set; }
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
        public string CustomerPoNumber { get; set; }
        public Nullable<byte> PriceLevelCode { get; set; }
        public string ShippingID { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<System.DateTime> ReqShipDate { get; set; }
        public string SiteID { get; set; }
        public bool BinTrack { get; set; }
        public string CurrencyCode { get; set; }
        public string ExchangeTableID { get; set; }
        public int ExchangeRateID { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public byte DecimalDigit { get; set; }
        public Nullable<double> TaxAMT { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<decimal> Markdown { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public string TrxComments { get; set; }
        public string SalePersonID { get; set; }
        public virtual SOP00015 SOP00015 { get; set; }
        public virtual ICollection<SOP10201> SOP10201 { get; set; }
    }
}
