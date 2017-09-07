using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00005
    {
        public CM00005()
        {
            this.CM00007 = new List<CM00007>();
            this.CM00105 = new List<CM00105>();
        }

        public int CalendarID { get; set; }
        public string CalendarName { get; set; }
        public int PeriodCount { get; set; }
        public virtual ICollection<CM00007> CM00007 { get; set; }
        public virtual ICollection<CM00105> CM00105 { get; set; }
    }
}
