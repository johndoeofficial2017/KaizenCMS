using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00027
    {
        public CM00027()
        {
            this.CM10701 = new List<CM10701>();
        }

        public int ChangeStatusSourceID { get; set; }
        public string ChangeStatusSourceName { get; set; }
        public virtual ICollection<CM10701> CM10701 { get; set; }
    }
}
