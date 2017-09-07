using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00201
    {
        public int TableID { get; set; }
        public int CalendarID { get; set; }
        public int MeetingID { get; set; }
        public virtual Met00011 Met00011 { get; set; }
        public virtual Met00200 Met00200 { get; set; }
    }
}
