using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10304
    {
        public string VoucherTrxNumber { get; set; }
        public string SOPNUMBE { get; set; }
        public double DOCAMNT { get; set; }
        public virtual SOP10300 SOP10300 { get; set; }
    }
}
