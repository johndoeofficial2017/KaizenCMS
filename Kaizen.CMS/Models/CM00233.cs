using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00233
    {
        public int TableID { get; set; }
        public string MessageTRXID { get; set; }
        public string CaseRef { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string MobileNo3 { get; set; }
        public string MobileNo4 { get; set; }
        public virtual CM00232 CM00232 { get; set; }
    }
}
