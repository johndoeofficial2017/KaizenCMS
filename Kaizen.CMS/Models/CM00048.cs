using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00048
    {
        public int TRXTypeID { get; set; }
        public string HtmlNew { get; set; }
        public virtual CM00029 CM00029 { get; set; }
    }
}
