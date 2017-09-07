using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00005
    {
        public int TaskID { get; set; }
        public string AsginFrom { get; set; }
        public string AsginTo { get; set; }
        public string TaskName { get; set; }
        public string TaskTitle { get; set; }
        public int TaskSourceID { get; set; }
        public string TaskTransactionID { get; set; }
        public System.DateTime TaskStartDate { get; set; }
        public System.DateTime TaskDueDate { get; set; }
        public string TaskDescription { get; set; }
        public bool TaskComplated { get; set; }
        public virtual Kaizen00004 Kaizen00004 { get; set; }
        public virtual User User { get; set; }
    }
}
