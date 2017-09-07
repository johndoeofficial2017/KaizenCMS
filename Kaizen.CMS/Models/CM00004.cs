using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00004
    {
        public CM00004()
        {
            this.CM00213 = new List<CM00213>();
        }

        public string TaskTypeID { get; set; }
        public string TaskTypeName { get; set; }
        public virtual ICollection<CM00213> CM00213 { get; set; }
    }
}
