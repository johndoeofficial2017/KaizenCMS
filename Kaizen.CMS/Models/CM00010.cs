using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00010
    {
        public CM00010()
        {
            this.CM00100 = new List<CM00100>();
        }

        public string CUSTCLAS { get; set; }
        public string CUSTCLASNAME { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public Nullable<int> LastDebtorID { get; set; }
        public virtual ICollection<CM00100> CM00100 { get; set; }
    }
}
