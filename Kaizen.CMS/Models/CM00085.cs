using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00085
    {
        public string FieldName { get; set; }
        public int ViewID { get; set; }
        public string FieldDisplay { get; set; }
        public virtual CM00071 CM00071 { get; set; }
    }
}
