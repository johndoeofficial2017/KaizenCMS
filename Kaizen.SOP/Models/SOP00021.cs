using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00021
    {
        public SOP00021()
        {
            this.SOP00022 = new List<SOP00022>();
        }

        public byte DueTypeID { get; set; }
        public string DueTypeName { get; set; }
        public virtual ICollection<SOP00022> SOP00022 { get; set; }
    }
}
