using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00076
    {
        public CM00076()
        {
            this.CM00077 = new List<CM00077>();
            this.CM00078 = new List<CM00078>();
            this.CM00079 = new List<CM00079>();
        }

        public int GraphID { get; set; }
        public int ViewID { get; set; }
        public string GraphName { get; set; }
        public int GraphTypeID { get; set; }
        public string GraphTitle { get; set; }
        public virtual CM00040 CM00040 { get; set; }
        public virtual CM00071 CM00071 { get; set; }
        public virtual ICollection<CM00077> CM00077 { get; set; }
        public virtual ICollection<CM00078> CM00078 { get; set; }
        public virtual ICollection<CM00079> CM00079 { get; set; }
    }
}
