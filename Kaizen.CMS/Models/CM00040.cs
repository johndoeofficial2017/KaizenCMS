using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00040
    {
        public CM00040()
        {
            this.CM00076 = new List<CM00076>();
        }

        public int GraphTypeID { get; set; }
        public string GraphTypeName { get; set; }
        public virtual ICollection<CM00076> CM00076 { get; set; }
    }
}
