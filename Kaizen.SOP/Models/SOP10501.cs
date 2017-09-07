using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10501
    {
        public SOP10501()
        {
            this.SOP10502 = new List<SOP10502>();
            this.SOP10504 = new List<SOP10504>();
        }

        public int ItemLineIndex { get; set; }
        public string SOPNUMBE { get; set; }
        public string ProjectID { get; set; }
        public string ItemID { get; set; }
        public string PhotoExtension { get; set; }
        public string ItemDescription { get; set; }
        public string ItemName { get; set; }
        public string CustomerItemID { get; set; }
        public string UFMGroupID { get; set; }
        public string UFMID { get; set; }
        public byte DecimalDigitQuantity { get; set; }
        public string SiteID { get; set; }
        public bool BinTrack { get; set; }
        public string LotNumber { get; set; }
        public Nullable<bool> IsExpiryDate { get; set; }
        public string VendorID { get; set; }
        public string ShippingID { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<System.DateTime> ReqShipDate { get; set; }
        public string LineDescription { get; set; }
        public Nullable<byte> PriceLevelCode { get; set; }
        public string SalePersonID { get; set; }
        public string Territory { get; set; }
        public double QuantityOrder { get; set; }
        public Nullable<double> QuantityFOC { get; set; }
        public Nullable<double> QuantityCancel { get; set; }
        public double UnitPrice { get; set; }
        public Nullable<double> UnitCost { get; set; }
        public Nullable<double> Markdown { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public Nullable<double> TaxAMT { get; set; }
        public Nullable<double> Commission { get; set; }
        public virtual SOP10500 SOP10500 { get; set; }
        public virtual ICollection<SOP10502> SOP10502 { get; set; }
        public virtual ICollection<SOP10504> SOP10504 { get; set; }
        public virtual SOP10506 SOP10506 { get; set; }
    }
}
