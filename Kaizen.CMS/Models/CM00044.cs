using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00044
    {
        public CM00044()
        {
            this.CM00046 = new List<CM00046>();
            this.CM00045 = new List<CM00045>();
        }

        public int ActionID { get; set; }
        public int FunctionTypeID { get; set; }
        public int RuleID { get; set; }
        public string ActionName { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public virtual CM00042 CM00042 { get; set; }
        public virtual ICollection<CM00046> CM00046 { get; set; }
        public virtual ICollection<CM00045> CM00045 { get; set; }
    }
}
