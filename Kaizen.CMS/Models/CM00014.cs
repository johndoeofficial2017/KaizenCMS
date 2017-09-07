using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00014
    {
        public CM00014()
        {
            this.CM00100 = new List<CM00100>();
        }

        public int DebtorStatusID { get; set; }
        public string DebtorStatusName { get; set; }
        public virtual ICollection<CM00100> CM00100 { get; set; }
    }
}
