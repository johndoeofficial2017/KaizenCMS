using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class DT00102
    {
        public int RoleID { get; set; }
        public int SourceID { get; set; }
        public virtual DT00100 DT00100 { get; set; }
    }
}
