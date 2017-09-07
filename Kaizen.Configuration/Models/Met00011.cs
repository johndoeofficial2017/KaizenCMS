using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00011
    {
        public Met00011()
        {
            this.Met00012 = new List<Met00012>();
            this.Met00013 = new List<Met00013>();
            this.Met00201 = new List<Met00201>();
        }

        public int CalendarID { get; set; }
        public string CalendarName { get; set; }
        public string CalendarColor { get; set; }
        public virtual ICollection<Met00012> Met00012 { get; set; }
        public virtual ICollection<Met00013> Met00013 { get; set; }
        public virtual ICollection<Met00201> Met00201 { get; set; }
    }
}
