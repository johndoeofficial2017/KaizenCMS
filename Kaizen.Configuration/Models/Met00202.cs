using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00202
    {
        public int AttendeeID { get; set; }
        public string AttendeeName { get; set; }
        public int MeetingID { get; set; }
        public virtual Met00200 Met00200 { get; set; }
    }
}
