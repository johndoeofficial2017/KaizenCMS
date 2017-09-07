using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP20301
    {
        public int LineID { get; set; }
        public string SOPNUMBE { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<byte> DecimalDigitQuantity { get; set; }
        public string ItemDescription { get; set; }
        public double InvoiceOTY { get; set; }
        public string UFMSale { get; set; }
        public double BaseUnitSale { get; set; }
        public double UnitCost { get; set; }
        public double UnitPrice { get; set; }
        public Nullable<double> Commission { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public Nullable<double> TaxAMT { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public virtual SOP20300 SOP20300 { get; set; }
    }
}
