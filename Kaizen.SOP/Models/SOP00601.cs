using System;

namespace Kaizen.SOP
{
    public partial class SOP00601
    {
        public int KendoID00601 { get; set; }
        public int KendoID00600 { get; set; }
        public string ItemID { get; set; }
        public string UFMID { get; set; }
        public string ItemName { get; set; }
        public string SiteID { get; set; }
        public byte PriceLevelCode { get; set; }
        public decimal QuantityOrder { get; set; }
        public decimal QuantityCancel { get; set; }
        public decimal QuantityValue { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ExtendedPrice { get; set; }
        public decimal Markdown { get; set; }
        public decimal Freight { get; set; }
        public decimal TradeDiscount { get; set; }
        public string ShipTo { get; set; }
        public string ShipMethodID { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<System.DateTime> ReqShipDate { get; set; }
        public string SalesPersonID { get; set; }
        public string Territory { get; set; }
        public decimal Commission { get; set; }
        public string LineDescription { get; set; }
        public Nullable<int> AccCostOfGoodsSold { get; set; }
        public Nullable<int> AccInventory { get; set; }
        public virtual SOP00600 SOP00600 { get; set; }
    }
}
