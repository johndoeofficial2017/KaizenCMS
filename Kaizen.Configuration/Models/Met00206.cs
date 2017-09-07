using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00206
    {
        public int RoleID { get; set; }
        public int MeetingRoomID { get; set; }
        public virtual Met00204 Met00204 { get; set; }
    }
}
