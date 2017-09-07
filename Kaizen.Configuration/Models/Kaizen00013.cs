using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00013
    {
        public int ScreenID { get; set; }
        public string FieldName { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldTitle { get; set; }
        public string kaizenTableName { get; set; }
        public virtual Kaizen00006 Kaizen00006 { get; set; }
        public virtual Kaizen00010 Kaizen00010 { get; set; }
    }
}
