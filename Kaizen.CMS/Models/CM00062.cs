using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00062
    {
        public CM00062()
        {
            this.CM00063 = new List<CM00063>();
            this.CM00064 = new List<CM00064>();
            this.CM00065 = new List<CM00065>();
        }

        public int ViewID { get; set; }
        public int CaseStatusID { get; set; }
        public string ViewName { get; set; }
        public virtual CM00700 CM00700 { get; set; }
        public virtual ICollection<CM00063> CM00063 { get; set; }
        public virtual ICollection<CM00064> CM00064 { get; set; }
        public virtual ICollection<CM00065> CM00065 { get; set; }
    }
}
