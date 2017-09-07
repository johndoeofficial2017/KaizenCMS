using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00006
    {
        public CM00006()
        {
            this.CM00213 = new List<CM00213>();
        }

        public string PriorityID { get; set; }
        public string PriorityName { get; set; }
        public virtual ICollection<CM00213> CM00213 { get; set; }
    }
}
