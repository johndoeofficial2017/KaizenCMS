using System;

namespace Kaizen.SOP
{
    public partial class IV00108
    {
        public string ItemID { get; set; }
        public string CUSTNMBR { get; set; }
        public string ShortDescription { get; set; }
        public string GenericDescription { get; set; }
        public string ItemDescription { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> SalesAcc { get; set; }
        public Nullable<int> SalesReturnAcc { get; set; }
        public Nullable<int> MarkdownAcc { get; set; }
        public Nullable<int> InventoryAcc { get; set; }
        public Nullable<int> InventoryReturnAcc { get; set; }
        public Nullable<int> InventoryOffsetAcc { get; set; }
        public Nullable<int> FreightAcc { get; set; }
        public Nullable<int> TradeDiscountAcc { get; set; }
        public Nullable<int> CostOfGoodsSold { get; set; }
        public Nullable<int> TaxAcc { get; set; }
        public virtual IV00100 IV00100 { get; set; }
        public virtual SOP00100 SOP00100 { get; set; }
    }
}
