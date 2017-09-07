using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10250
    {
        public SOP10250()
        {
            this.SOP10251 = new List<SOP10251>();
            this.SOP10252 = new List<SOP10252>();
        }

        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public Nullable<byte> SOPTYPE { get; set; }
        public string SOPNUMBEORG { get; set; }
        public string TransactionID { get; set; }
        public string BatchID { get; set; }
        public string ProjectID { get; set; }
        public string PaymentTermID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public double DOCAMNT { get; set; }
        public double DOCAMNTCollected { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public string BillAddressTypeCode { get; set; }
        public string BillAddressName { get; set; }
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
        public Nullable<System.DateTime> ReqShipDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public string SiteID { get; set; }
        public bool BinTrack { get; set; }
        public string CurrencyCode { get; set; }
        public string ExchangeTableID { get; set; }
        public int ExchangeRateID { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public byte DecimalDigit { get; set; }
        public string Territory { get; set; }
        public string SalePersonID { get; set; }
        public Nullable<double> TaxAMT { get; set; }
        public Nullable<double> Markdown { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public string TrxComments { get; set; }
        public string VendorID { get; set; }
        public string CountryID { get; set; }
        public virtual SOP00004 SOP00004 { get; set; }
        public virtual SOP00015 SOP00015 { get; set; }
        public virtual ICollection<SOP10251> SOP10251 { get; set; }
        public virtual ICollection<SOP10252> SOP10252 { get; set; }
    }
}
