using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00009
    {
        public string UserName { get; set; }
        public int MeetingRoomID { get; set; }
        public virtual Met00007 Met00007 { get; set; }
    }
}
