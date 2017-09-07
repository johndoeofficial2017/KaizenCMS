using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10500
    {
        public SOP10500()
        {
            this.SOP10501 = new List<SOP10501>();
        }

        public string SOPNUMBE { get; set; }
        public string SOPNUMBEORG { get; set; }
        public string BatchID { get; set; }
        public string ProjectID { get; set; }
        public string PaymentTermID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public double DOCAMNT { get; set; }
        public Nullable<double> DOCAMNTCollected { get; set; }
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
        public string ShipAddressCode { get; set; }
        public string ShipAddressName { get; set; }
        public string ShipPnone01 { get; set; }
        public string ShipPnone02 { get; set; }
        public string ShipMobileNo1 { get; set; }
        public string ShipMobileNo2 { get; set; }
        public string ShipPOBox { get; set; }
        public string ShipOther01 { get; set; }
        public string ShipOther02 { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipEmail01 { get; set; }
        public string ShipEmail02 { get; set; }
        public string CustomerPoNumber { get; set; }
        public Nullable<byte> PriceLevelCode { get; set; }
        public Nullable<System.DateTime> ReqShipDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public string SiteID { get; set; }
        public Nullable<bool> BinTrack { get; set; }
        public string CurrencyCode { get; set; }
        public string ExchangeTableID { get; set; }
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
        public virtual SOP00003 SOP00003 { get; set; }
        public virtual SOP00015 SOP00015 { get; set; }
        public virtual SOP10510 SOP10510 { get; set; }
        public virtual ICollection<SOP10501> SOP10501 { get; set; }
    }
}
