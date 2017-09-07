using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00039
    {
        public int TRXTypeID { get; set; }
        public string FieldName { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldDisplay { get; set; }
        public bool IsRequired { get; set; }
        public int FieldOrder { get; set; }
        public bool IsOVerridable { get; set; }
        public virtual CM00029 CM00029 { get; set; }
    }
}
