using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00058
    {
        public CM00058()
        {
            this.CM00108 = new List<CM00108>();
        }

        public int TargetTypeID { get; set; }
        public string TargetTypeName { get; set; }
        public virtual ICollection<CM00108> CM00108 { get; set; }
    }
}
