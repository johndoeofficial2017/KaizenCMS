using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00102
    {
        public int AttendeeID { get; set; }
        public string AttendeeName { get; set; }
        public int MeetingID { get; set; }
        public virtual Met00100 Met00100 { get; set; }
    }
}
