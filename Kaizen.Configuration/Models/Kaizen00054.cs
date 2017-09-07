using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00054
    {
        public int RoleID { get; set; }
        public int DashboardCode { get; set; }
        public virtual Kaizen00050 Kaizen00050 { get; set; }
    }
}
