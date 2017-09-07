using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00066
    {
        public CM00066()
        {
            this.CM00140 = new List<CM00140>();
        }

        public string LegalClassID { get; set; }
        public string LegalClassName { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public Nullable<int> LastLegalID { get; set; }
        public virtual ICollection<CM00140> CM00140 { get; set; }
    }
}
