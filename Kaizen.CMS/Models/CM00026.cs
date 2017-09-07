using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00026
    {
        public int StatusScriptID { get; set; }
        public int CaseStatusID { get; set; }
        public string StatusScriptName { get; set; }
        public string StatusScriptMain { get; set; }
        public virtual CM00700 CM00700 { get; set; }
    }
}
