using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00011
    {
        public SOP00011()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
