using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00056
    {
        public int TRXTypeID { get; set; }
        public int RoleID { get; set; }
        public virtual CM00029 CM00029 { get; set; }
    }
}
