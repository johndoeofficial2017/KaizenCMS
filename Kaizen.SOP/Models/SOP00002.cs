using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00002
    {
        public string SalesPersonID { get; set; }
        public string SalesPersonName { get; set; }
        public virtual SOP00005 SOP00005 { get; set; }
    }
}
