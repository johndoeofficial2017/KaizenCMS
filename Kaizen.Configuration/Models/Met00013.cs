using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00013
    {
        public int CalendarID { get; set; }
        public int RoleID { get; set; }
        public virtual Met00011 Met00011 { get; set; }
    }
}
