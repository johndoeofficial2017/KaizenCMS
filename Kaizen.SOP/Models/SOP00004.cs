using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00004
    {
        public SOP00004()
        {
            this.SOP10100 = new List<SOP10100>();
            this.SOP10250 = new List<SOP10250>();
        }

        public string BatchID { get; set; }
        public string BatchName { get; set; }
        public byte SOPTYPE { get; set; }
        public virtual SOP00000 SOP00000 { get; set; }
        public virtual ICollection<SOP10100> SOP10100 { get; set; }
        public virtual ICollection<SOP10250> SOP10250 { get; set; }
    }
}
