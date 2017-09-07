using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00119
    {
        public int RoleID { get; set; }
        public string DebtorID { get; set; }
        public virtual CM00100 CM00100 { get; set; }
    }
}
