using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00009
    {
        public int DropValueID { get; set; }
        public string ExtraFieldID { get; set; }
        public string FieldValue { get; set; }
        public virtual Kaizen00003 Kaizen00003 { get; set; }
    }
}
