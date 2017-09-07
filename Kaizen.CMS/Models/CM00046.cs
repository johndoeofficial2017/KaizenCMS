using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00046
    {
        public int TaskID { get; set; }
        public int ActionID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public System.DateTime TaskStartDate { get; set; }
        public System.DateTime TaskEndDate { get; set; }
        public string AssignTO { get; set; }
        public System.DateTime AssignDate { get; set; }
        public virtual CM00044 CM00044 { get; set; }
    }
}
