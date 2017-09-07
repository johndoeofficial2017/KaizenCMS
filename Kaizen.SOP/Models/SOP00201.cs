using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00201
    {
        public SOP00201()
        {
            this.SOP00200 = new List<SOP00200>();
        }

        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public virtual ICollection<SOP00200> SOP00200 { get; set; }
    }
}
