using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00065
    {
        public int ViewID { get; set; }
        public int RoleID { get; set; }
        public virtual CM00062 CM00062 { get; set; }
    }
}
