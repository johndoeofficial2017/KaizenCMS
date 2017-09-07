using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class Sys00007
    {
        public Sys00007()
        {
            this.SOP00106 = new List<SOP00106>();
        }

        public int DocumentTypeID { get; set; }
        public int ContactSourceID { get; set; }
        public string DocumentTypeName { get; set; }
        public virtual ICollection<SOP00106> SOP00106 { get; set; }
        public virtual Sys00005 Sys00005 { get; set; }
    }
}
