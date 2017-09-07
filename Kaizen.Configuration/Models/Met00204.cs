using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00204
    {
        public Met00204()
        {
            this.Met00200 = new List<Met00200>();
            this.Met00205 = new List<Met00205>();
            this.Met00206 = new List<Met00206>();
        }

        public int MeetingRoomID { get; set; }
        public string MeetingRoomName { get; set; }
        public virtual ICollection<Met00200> Met00200 { get; set; }
        public virtual ICollection<Met00205> Met00205 { get; set; }
        public virtual ICollection<Met00206> Met00206 { get; set; }
    }
}
