using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00059
    {
        public CM00059()
        {
            this.CM00700 = new List<CM00700>();
        }

        public int CaseStatusTypeID { get; set; }
        public string CaseStatusTypeName { get; set; }
        public virtual ICollection<CM00700> CM00700 { get; set; }
    }
}
