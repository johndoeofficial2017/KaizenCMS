using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00079
    {
        public int GraphID { get; set; }
        public string FieldName { get; set; }
        public string FieldDisplay { get; set; }
        public string FieldValue { get; set; }
        public int SummeryTypeID { get; set; }
        public string FieldColor { get; set; }
        public virtual CM00076 CM00076 { get; set; }
    }
}
