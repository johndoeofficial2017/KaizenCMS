using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00012
    {
        public SOP00012()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public string PriorityID { get; set; }
        public string PriorityName { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
