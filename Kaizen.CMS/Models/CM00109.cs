using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00109
    {
        public CM00109()
        {
            this.CM00107 = new List<CM00107>();
        }

        public string TargetID { get; set; }
        public string AgentID { get; set; }
        public int TargetTypeID { get; set; }
        public bool IsPercentTarget { get; set; }
        public string SQLCondition { get; set; }
        public virtual CM00105 CM00105 { get; set; }
        public virtual ICollection<CM00107> CM00107 { get; set; }
        public virtual CM00108 CM00108 { get; set; }
    }
}
