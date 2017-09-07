using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00002
    {
        public string UserName { get; set; }
        public int CalendarID { get; set; }
        public virtual Met00001 Met00001 { get; set; }
    }
}
