using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00025
    {
        public int TaskID { get; set; }
        public string TaskTypeID { get; set; }
        public string PriorityID { get; set; }
        public int CaseStatusID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string AgentID { get; set; }
        public double TaskProgress { get; set; }
        public int RequiredDays { get; set; }
        public Nullable<int> CaseStatusIDEffect { get; set; }
        public string SqlScript { get; set; }
        public virtual CM00700 CM00700 { get; set; }
    }
}
