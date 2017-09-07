using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00018
    {
        public CM00018()
        {
            this.CM00200 = new List<CM00200>();
        }

        public int PaymentBaseID { get; set; }
        public string PaymentBaseName { get; set; }
        public virtual ICollection<CM00200> CM00200 { get; set; }
    }
}
