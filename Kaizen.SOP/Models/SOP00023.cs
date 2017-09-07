using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00023
    {
        public SOP00023()
        {
            this.SOP00200 = new List<SOP00200>();
        }

        public string TerritoryID { get; set; }
        public string TerritoryName { get; set; }
        public virtual ICollection<SOP00200> SOP00200 { get; set; }
    }
}
