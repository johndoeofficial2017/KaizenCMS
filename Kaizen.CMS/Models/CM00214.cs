using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00214
    {
        public int ActionID { get; set; }
        public int TaskID { get; set; }
        public double TaskProgress { get; set; }
        public string TaskDescription { get; set; }
        public System.DateTime TaskDate { get; set; }
        public string AgentID { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<bool> IsCanceled { get; set; }
        public virtual CM00213 CM00213 { get; set; }
    }
}
