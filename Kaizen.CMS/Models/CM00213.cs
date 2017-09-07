using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00213
    {
        public CM00213()
        {
            this.CM00214 = new List<CM00214>();
        }

        public int TaskID { get; set; }
        public Nullable<int> TaskIDSource { get; set; }
        public string TaskTypeID { get; set; }
        public string PriorityID { get; set; }
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public System.DateTime TaskStartDate { get; set; }
        public System.DateTime TaskEndDate { get; set; }
        public string AgentID { get; set; }
        public System.DateTime AssignDate { get; set; }
        public double TaskProgress { get; set; }
        public virtual CM00004 CM00004 { get; set; }
        public virtual CM00006 CM00006 { get; set; }
        public virtual CM00203 CM00203 { get; set; }
        public virtual ICollection<CM00214> CM00214 { get; set; }
    }
}
