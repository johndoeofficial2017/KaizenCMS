using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00003
    {
        public CM00003()
        {
            this.CM00700 = new List<CM00700>();
            this.CM00053 = new List<CM00053>();
            this.CM00054 = new List<CM00054>();
        }

        public int StatusActionTypeID { get; set; }
        public string StatusActionTypeName { get; set; }
        public virtual ICollection<CM00700> CM00700 { get; set; }
        public virtual ICollection<CM00053> CM00053 { get; set; }
        public virtual ICollection<CM00054> CM00054 { get; set; }
    }
}
