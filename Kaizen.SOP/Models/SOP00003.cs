using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00003
    {
        public SOP00003()
        {
            this.SOP10101 = new List<SOP10101>();
            this.SOP10251 = new List<SOP10251>();
            this.SOP10500 = new List<SOP10500>();
        }

        public string Territory { get; set; }
        public string TerritoryName { get; set; }
        public virtual ICollection<SOP10101> SOP10101 { get; set; }
        public virtual ICollection<SOP10251> SOP10251 { get; set; }
        public virtual ICollection<SOP10500> SOP10500 { get; set; }
    }
}
