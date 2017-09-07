using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10201
    {
        public SOP10201()
        {
            this.SOP10203 = new List<SOP10203>();
        }

        public int LineID { get; set; }
        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public string ProjectID { get; set; }
        public Nullable<int> ItemCategoryID { get; set; }
        public string ItemID { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<byte> DecimalDigitQuantity { get; set; }
        public string ItemDescription { get; set; }
        public string ItemName { get; set; }
        public string CustomerItemID { get; set; }
        public string ShipAddressTypeCode { get; set; }
        public string ShipAddressName { get; set; }
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
        public string UFMGroupID { get; set; }
        public string UFMID { get; set; }
        public string SiteID { get; set; }
        public bool BinTrack { get; set; }
        public string VendorID { get; set; }
        public string VendorItemID { get; set; }
        public string VendorItemName { get; set; }
        public string VendorShortDescription { get; set; }
        public string CountryID { get; set; }
        public Nullable<double> EstimatedCost { get; set; }
        public Nullable<double> EstimatedPrice { get; set; }
        public double QuantityShort { get; set; }
        public string ShippingID { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<System.DateTime> ReqShipDate { get; set; }
        public string LineDescription { get; set; }
        public Nullable<byte> PriceLevelCode { get; set; }
        public double QuantityOrder { get; set; }
        public double QuantityCancel { get; set; }
        public double UnitPrice { get; set; }
        public double UnitCost { get; set; }
        public Nullable<decimal> Markdown { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public Nullable<double> TaxAMT { get; set; }
        public Nullable<double> Commission { get; set; }
        public virtual IV00100 IV00100 { get; set; }
        public virtual PROJ00100 PROJ00100 { get; set; }
        public virtual SOP00024 SOP00024 { get; set; }
        public virtual SOP10200 SOP10200 { get; set; }
        public virtual SOP10202 SOP10202 { get; set; }
        public virtual ICollection<SOP10203> SOP10203 { get; set; }
    }
}
