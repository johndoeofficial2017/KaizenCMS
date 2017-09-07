using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00072
    {
        public int RoleID { get; set; }
        public int ViewID { get; set; }
        public virtual CM00071 CM00071 { get; set; }
    }
}
