using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00001
    {
        public Met00001()
        {
            this.Met00002 = new List<Met00002>();
            this.Met00003 = new List<Met00003>();
            this.Met00101 = new List<Met00101>();
        }

        public int CalendarID { get; set; }
        public string CalendarName { get; set; }
        public string CalendarColor { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Met00002> Met00002 { get; set; }
        public virtual ICollection<Met00003> Met00003 { get; set; }
        public virtual ICollection<Met00101> Met00101 { get; set; }
    }
}
