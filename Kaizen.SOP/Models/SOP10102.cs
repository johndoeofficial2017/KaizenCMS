using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10102
    {
        public int LOTLineCode { get; set; }
        public int LineID { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string BarCode { get; set; }
        public double AppliedQuantity { get; set; }
        public virtual SOP10101 SOP10101 { get; set; }
    }
}
