using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10104
    {
        public SOP10104()
        {
            this.SOP10105 = new List<SOP10105>();
        }

        public int LineID { get; set; }
        public int BinID { get; set; }
        public bool IsBinGroup { get; set; }
        public double AppliedQuantity { get; set; }
        public virtual SOP10101 SOP10101 { get; set; }
        public virtual ICollection<SOP10105> SOP10105 { get; set; }
    }
}
