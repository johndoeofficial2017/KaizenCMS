using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00024
    {
        public int FunctionCode { get; set; }
        public short FieldTypeID { get; set; }
        public string FunctionDisplay { get; set; }
        public string FunctionEntryValue { get; set; }
        public virtual CM00028 CM00028 { get; set; }
    }
}
