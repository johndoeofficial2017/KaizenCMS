using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00023
    {
        public CM00023()
        {
            this.CM00105 = new List<CM00105>();
        }

        public string AgentGroup { get; set; }
        public string AgentGroupName { get; set; }
        public string NextDocumentNumber { get; set; }
        public string NextNumberPrefix { get; set; }
        public Nullable<byte> NextNumberlenth { get; set; }
        public virtual ICollection<CM00105> CM00105 { get; set; }
    }
}
