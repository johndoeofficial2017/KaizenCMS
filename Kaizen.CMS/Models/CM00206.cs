using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00206
    {
        public int AssignLineID { get; set; }
        public string AssigmentID { get; set; }
        public string CaseRef { get; set; }
        public string AgentID { get; set; }
        public System.DateTime AssignHistoryDate { get; set; }
        public virtual CM00202 CM00202 { get; set; }
    }
}
