using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00019
    {
        public CM00019()
        {
            this.CM00200 = new List<CM00200>();
        }

        public int ContractStatusID { get; set; }
        public string ContractStatusName { get; set; }
        public virtual ICollection<CM00200> CM00200 { get; set; }
    }
}
