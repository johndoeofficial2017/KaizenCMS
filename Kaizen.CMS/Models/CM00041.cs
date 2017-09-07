using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00041
    {
        public CM00041()
        {
            this.CM00042 = new List<CM00042>();
        }

        public int TableRuleID { get; set; }
        public string TableRuleName { get; set; }
        public virtual ICollection<CM00042> CM00042 { get; set; }
    }
}
