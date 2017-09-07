using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00013
    {
        public SOP00013()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public int StatementCycleID { get; set; }
        public string StatementCycleName { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
