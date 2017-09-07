using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10505
    {
        public string BinID { get; set; }
        public string SubBinID { get; set; }
        public double ItemQuantity { get; set; }
        public virtual SOP10504 SOP10504 { get; set; }
    }
}
