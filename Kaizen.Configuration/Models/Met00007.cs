using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00007
    {
        public Met00007()
        {
            this.Met00008 = new List<Met00008>();
            this.Met00009 = new List<Met00009>();
            this.Met00100 = new List<Met00100>();
        }

        public int MeetingRoomID { get; set; }
        public string MeetingRoomName { get; set; }
        public virtual ICollection<Met00008> Met00008 { get; set; }
        public virtual ICollection<Met00009> Met00009 { get; set; }
        public virtual ICollection<Met00100> Met00100 { get; set; }
    }
}
