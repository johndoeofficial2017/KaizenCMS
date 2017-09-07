using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00205
    {
        public int LineID { get; set; }
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public string AgentID { get; set; }
        public string PhotoExtension { get; set; }
        public virtual CM00203 CM00203 { get; set; }
    }
}
