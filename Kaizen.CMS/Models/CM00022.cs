using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00022
    {
        public CM00022()
        {
            this.CM00110 = new List<CM00110>();
            this.CM00130 = new List<CM00130>();
            this.CM00140 = new List<CM00140>();
        }

        public string StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<CM00110> CM00110 { get; set; }
        public virtual ICollection<CM00130> CM00130 { get; set; }
        public virtual ICollection<CM00140> CM00140 { get; set; }
    }
}
