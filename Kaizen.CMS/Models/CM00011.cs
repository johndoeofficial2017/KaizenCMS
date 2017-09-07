using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00011
    {
        public CM00011()
        {
            this.CM00100 = new List<CM00100>();
        }

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<CM00100> CM00100 { get; set; }
    }
}
