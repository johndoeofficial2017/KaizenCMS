using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00054
    {
        public int StatusActionTypeID { get; set; }
        public int RoleID { get; set; }
        public virtual CM00003 CM00003 { get; set; }
    }
}
