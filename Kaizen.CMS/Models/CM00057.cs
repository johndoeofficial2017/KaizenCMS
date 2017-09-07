using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00057
    {
        public int TRXTypeID { get; set; }
        public string UserName { get; set; }
        public virtual CM00029 CM00029 { get; set; }
    }
}
