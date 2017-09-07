using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00014
    {
        public SOP00014()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
