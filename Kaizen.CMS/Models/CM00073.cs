using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00073
    {
        public int ViewID { get; set; }
        public string UserName { get; set; }
        public bool IsDefault { get; set; }
        public virtual CM00071 CM00071 { get; set; }
    }
}
