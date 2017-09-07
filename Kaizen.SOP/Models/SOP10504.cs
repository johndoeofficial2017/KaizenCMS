using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10504
    {
        public SOP10504()
        {
            this.SOP10505 = new List<SOP10505>();
        }

        public string BinID { get; set; }
        public int ItemLineIndex { get; set; }
        public string SOPNUMBE { get; set; }
        public bool IsBinGroup { get; set; }
        public double AppliedQuantity { get; set; }
        public virtual SOP10501 SOP10501 { get; set; }
        public virtual ICollection<SOP10505> SOP10505 { get; set; }
    }
}
