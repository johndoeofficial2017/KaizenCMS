using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00060
    {
        public CM00060()
        {
            this.CM00061 = new List<CM00061>();
        }

        public int CaseStatusID { get; set; }
        public string FieldCode { get; set; }
        public bool IsRequired { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldName { get; set; }
        public virtual CM00028 CM00028 { get; set; }
        public virtual CM00700 CM00700 { get; set; }
        public virtual ICollection<CM00061> CM00061 { get; set; }
    }
}
