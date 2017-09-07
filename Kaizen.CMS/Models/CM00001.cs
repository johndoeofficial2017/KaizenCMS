using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00001
    {
        public CM00001()
        {
            this.CM00043 = new List<CM00043>();
        }

        public int OperatorID { get; set; }
        public string OperatorNme { get; set; }
        public virtual ICollection<CM00043> CM00043 { get; set; }
    }
}
