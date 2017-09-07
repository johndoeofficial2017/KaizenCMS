using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00008
    {
        public SOP00008()
        {
            this.SOP00110 = new List<SOP00110>();
        }

        public int SalePersonTypeID { get; set; }
        public string SalePersonTypeName { get; set; }
        public virtual ICollection<SOP00110> SOP00110 { get; set; }
    }
}
