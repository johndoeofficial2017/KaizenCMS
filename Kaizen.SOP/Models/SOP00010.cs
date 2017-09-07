using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00010
    {
        public SOP00010()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public string CUSTCLAS { get; set; }
        public string CUSTCLASNAME { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public Nullable<int> LastCustomerID { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
