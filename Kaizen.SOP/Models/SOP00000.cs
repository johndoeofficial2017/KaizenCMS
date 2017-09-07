using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00000
    {
        public SOP00000()
        {
            this.SOP00001 = new List<SOP00001>();
            this.SOP00004 = new List<SOP00004>();
        }

        public byte SOPTYPE { get; set; }
        public string SOPTYPENAME { get; set; }
        public virtual ICollection<SOP00001> SOP00001 { get; set; }
        public virtual ICollection<SOP00004> SOP00004 { get; set; }
    }
}
