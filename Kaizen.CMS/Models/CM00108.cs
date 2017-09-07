using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00108
    {
        public CM00108()
        {
            this.CM00109 = new List<CM00109>();
        }

        public string TargetID { get; set; }
        public Nullable<int> TargetTypeID { get; set; }
        public Nullable<bool> IsPercentTarget { get; set; }
        public string TargetName { get; set; }
        public string SQLCondition { get; set; }
        public virtual CM00058 CM00058 { get; set; }
        public virtual ICollection<CM00109> CM00109 { get; set; }
    }
}
