using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00202
    {
        public CM00202()
        {
            this.CM00206 = new List<CM00206>();
        }

        public string AssigmentID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public string UserName { get; set; }
        public string AgentID { get; set; }
        public string AssignDescription { get; set; }
        public virtual ICollection<CM00206> CM00206 { get; set; }
    }
}
