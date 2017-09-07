using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00005
    {
        public Met00005()
        {
            this.Met00100 = new List<Met00100>();
            this.Met00200 = new List<Met00200>();
        }

        public int MeetingRepeatTypeID { get; set; }
        public string MeetingRepeatTypeName { get; set; }
        public virtual ICollection<Met00100> Met00100 { get; set; }
        public virtual ICollection<Met00200> Met00200 { get; set; }
    }
}
