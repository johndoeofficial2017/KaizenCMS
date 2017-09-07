using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00047
    {
        public CM00047()
        {
            this.CM00078 = new List<CM00078>();
        }

        public int SummeryTypeID { get; set; }
        public string SummeryTypeName { get; set; }
        public virtual ICollection<CM00078> CM00078 { get; set; }
    }
}
