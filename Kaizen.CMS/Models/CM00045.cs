using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00045
    {
        public int ActionPar { get; set; }
        public string FunctionName { get; set; }
        public int ActionID { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public virtual CM00044 CM00044 { get; set; }
    }
}
