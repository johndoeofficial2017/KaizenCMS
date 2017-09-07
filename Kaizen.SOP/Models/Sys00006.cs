using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class Sys00006
    {
        public Sys00006()
        {
            this.SOP00105 = new List<SOP00105>();
        }

        public int ContactTypeID { get; set; }
        public string ContactTypeName { get; set; }
        public int ContactSourceID { get; set; }
        public virtual ICollection<SOP00105> SOP00105 { get; set; }
        public virtual Sys00005 Sys00005 { get; set; }
    }
}
