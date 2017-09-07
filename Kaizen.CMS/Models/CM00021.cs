using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00021
    {
        public CM00021()
        {
            this.CM00110 = new List<CM00110>();
        }

        public string CUSTCLAS { get; set; }
        public string CUSTCLASNAME { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public Nullable<int> LastClientID { get; set; }
        public virtual ICollection<CM00110> CM00110 { get; set; }
    }
}
