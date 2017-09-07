using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00042
    {
        public CM00042()
        {
            this.CM00044 = new List<CM00044>();
        }

        public int RuleID { get; set; }
        public int TableRuleID { get; set; }
        public string RuleName { get; set; }
        public bool IsStopNext { get; set; }
        public int RuleOrder { get; set; }
        public virtual CM00041 CM00041 { get; set; }
        public virtual ICollection<CM00044> CM00044 { get; set; }
    }
}
