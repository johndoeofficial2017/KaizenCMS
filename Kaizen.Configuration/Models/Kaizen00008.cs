using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00008
    {
        public string FieldID { get; set; }
        public int EXT_ScreenID { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string kaizenTableName { get; set; }
        public virtual Kaizen00006 Kaizen00006 { get; set; }
        public virtual Kaizen00007 Kaizen00007 { get; set; }
    }
}
