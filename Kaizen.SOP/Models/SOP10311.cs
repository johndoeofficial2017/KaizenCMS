using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10311
    {
        public int LineID { get; set; }
        public string TrxNumber { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<byte> DecimalDigitQuantity { get; set; }
        public double InvoiceOTY { get; set; }
        public string UFMSale { get; set; }
        public double BaseUnitSale { get; set; }
        public double UnitPrice { get; set; }
        public virtual SOP10310 SOP10310 { get; set; }
    }
}
