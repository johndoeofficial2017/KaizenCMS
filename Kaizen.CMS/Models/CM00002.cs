using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00002
    {
        public CM00002()
        {
            this.CM00130 = new List<CM00130>();
        }

        public string PartnerClassID { get; set; }
        public string PartnerClassName { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public Nullable<int> LastPartnerID { get; set; }
        public virtual ICollection<CM00130> CM00130 { get; set; }
    }
}
