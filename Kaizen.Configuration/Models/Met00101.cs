using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00101
    {
        public int TableID { get; set; }
        public int CalendarID { get; set; }
        public int MeetingID { get; set; }
        public virtual Met00001 Met00001 { get; set; }
        public virtual Met00100 Met00100 { get; set; }
    }
}
