using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10105
    {
        public int LineID { get; set; }
        public int BinID { get; set; }
        public string SubBinID { get; set; }
        public double ItemQuantity { get; set; }
        public virtual SOP10104 SOP10104 { get; set; }
    }
}
