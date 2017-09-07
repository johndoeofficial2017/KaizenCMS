using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00078
    {
        public string FieldName { get; set; }
        public int GraphID { get; set; }
        public string FieldDisplay { get; set; }
        public int SummeryTypeID { get; set; }
        public virtual CM00047 CM00047 { get; set; }
        public virtual CM00076 CM00076 { get; set; }
    }
}
