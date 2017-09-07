using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00013
    {
        public CM00013()
        {
            this.CM00200 = new List<CM00200>();
        }

        public int BillingFrequencyID { get; set; }
        public string BillingFrequencyName { get; set; }
        public virtual ICollection<CM00200> CM00200 { get; set; }
    }
}
