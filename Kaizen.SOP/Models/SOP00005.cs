using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00005
    {
        public string SalesPersonID { get; set; }
        public string UserCode { get; set; }
        public Nullable<byte> SOPTYPE { get; set; }
        public string SiteID { get; set; }
        public virtual SOP00002 SOP00002 { get; set; }
    }
}
