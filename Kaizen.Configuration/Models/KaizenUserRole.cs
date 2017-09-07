using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class KaizenUserRole
    {
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public virtual Kaizen00030 Kaizen00030 { get; set; }
    }
}
