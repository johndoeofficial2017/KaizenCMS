using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class MS_00201
    {
        public int LineID { get; set; }
        public string TrxID { get; set; }
        public string ReceiverEmail { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public virtual MS_00200 MS_00200 { get; set; }
    }
}
