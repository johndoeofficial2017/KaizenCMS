using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00009
    {
        public SOP00009()
        {
            this.SOP00101 = new List<SOP00101>();
        }

        public string AddressTypeCode { get; set; }
        public string AddressTypeName { get; set; }
        public virtual ICollection<SOP00101> SOP00101 { get; set; }
    }
}
