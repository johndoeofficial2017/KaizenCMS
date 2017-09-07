using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00061
    {
        public string FieldCode { get; set; }
        public int CaseStatusID { get; set; }
        public string LookupID { get; set; }
        public string LookupName { get; set; }
        public virtual CM00060 CM00060 { get; set; }
    }
}
