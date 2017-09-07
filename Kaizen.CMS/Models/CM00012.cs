using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00012
    {
        public CM00012()
        {
            this.CM00100 = new List<CM00100>();
        }

        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public string PriorityColor { get; set; }
        public virtual ICollection<CM00100> CM00100 { get; set; }
    }
}
