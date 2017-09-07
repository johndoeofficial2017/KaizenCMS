using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00012
    {
        public string UserName { get; set; }
        public int CalendarID { get; set; }
        public virtual Met00011 Met00011 { get; set; }
    }
}
