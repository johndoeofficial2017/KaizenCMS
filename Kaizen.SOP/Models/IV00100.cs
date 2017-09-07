using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class IV00100
    {
        public IV00100()
        {
            this.IV00108 = new List<IV00108>();
            this.SOP10101 = new List<SOP10101>();
            this.SOP10201 = new List<SOP10201>();
            this.SOP10251 = new List<SOP10251>();
        }

        public string ItemID { get; set; }
        public string UFMGroupID { get; set; }
        public Nullable<byte> DecimalDigitQuantity { get; set; }
        public int PriceMethodCode { get; set; }
        public byte ItemTypeID { get; set; }
        public bool IsinActive { get; set; }
        public byte ClassID { get; set; }
        public string ShortDescription { get; set; }
        public string GenericDescription { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<double> ShippingWeight { get; set; }
        public string ABCID { get; set; }
        public string UFMPurchase { get; set; }
        public string UFMSale { get; set; }
        public Nullable<byte> PriceLevelCode { get; set; }
        public string PhotoExtension { get; set; }
        public byte ValuationMethodID { get; set; }
        public int TrackTypeID { get; set; }
        public string LotNumber { get; set; }
        public double UnitCost { get; set; }
        public double UnitPrice { get; set; }
        public string PrimaryVendor { get; set; }
        public string CountryID { get; set; }
        public short KaizenID { get; set; }
        public virtual ICollection<IV00108> IV00108 { get; set; }
        public virtual ICollection<SOP10101> SOP10101 { get; set; }
        public virtual ICollection<SOP10201> SOP10201 { get; set; }
        public virtual ICollection<SOP10251> SOP10251 { get; set; }
    }
}
