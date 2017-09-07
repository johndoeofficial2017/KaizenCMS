using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00300
    {
        public SOP00300()
        {
            this.SOP00301 = new List<SOP00301>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesign { get; set; }
        public virtual ICollection<SOP00301> SOP00301 { get; set; }
    }
}
