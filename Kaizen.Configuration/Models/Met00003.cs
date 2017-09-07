using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00003
    {
        public int CalendarID { get; set; }
        public int RoleID { get; set; }
        public virtual Met00001 Met00001 { get; set; }
    }
}
