using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00053
    {
        public int StatusActionTypeID { get; set; }
        public string UserName { get; set; }
        public virtual CM00003 CM00003 { get; set; }
    }
}
