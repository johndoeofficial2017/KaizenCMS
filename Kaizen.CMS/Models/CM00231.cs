using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00231
    {
        public int TableID { get; set; }
        public string MessageTRXID { get; set; }
        public string CaseRef { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
        public string Email03 { get; set; }
        public string Email04 { get; set; }
        public virtual CM00230 CM00230 { get; set; }
    }
}
