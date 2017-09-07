using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00051
    {
        public string UserName { get; set; }
        public int CaseStatusID { get; set; }
        public virtual CM00700 CM00700 { get; set; }
    }
}
