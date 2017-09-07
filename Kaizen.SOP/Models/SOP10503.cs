using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10503
    {
        public string CollCode { get; set; }
        public int LotRowID { get; set; }
        public string CollName { get; set; }
        public string CollValue { get; set; }
        public int CollType { get; set; }
        public virtual SOP10502 SOP10502 { get; set; }
    }
}
