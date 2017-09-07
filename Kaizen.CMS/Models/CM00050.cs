using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00050
    {
        public int LineID { get; set; }
        public int TRXTypeID { get; set; }
        public string FieldName { get; set; }
        public string LookupFieldValue { get; set; }
        public virtual CM00070 CM00070 { get; set; }
    }
}
