using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00235
    {
        public int TableID { get; set; }
        public string MessageTRXID { get; set; }
        public string CaseRef { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public virtual CM00234 CM00234 { get; set; }
    }
}
