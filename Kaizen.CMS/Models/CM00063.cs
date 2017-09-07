using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00063
    {
        public int ViewID { get; set; }
        public int CaseStatusID { get; set; }
        public string FieldCode { get; set; }
        public int FieldTypeID { get; set; }
        public int FieldOrder { get; set; }
        public string FieldTitle { get; set; }
        public virtual CM00062 CM00062 { get; set; }
    }
}
