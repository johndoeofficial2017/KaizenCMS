using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00601View
    {
        public string ItemID { get; set; }
        public string SiteID { get; set; }
        public Nullable<decimal> QuantityValue { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
    }
}
