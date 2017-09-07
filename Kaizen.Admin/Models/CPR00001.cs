using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class CPR00001
    {
        public CPR00001()
        {
            this.CPR00100 = new List<CPR00100>();
        }

        public int RecordTypeID { get; set; }
        public string RecordTypeName { get; set; }
        public virtual ICollection<CPR00100> CPR00100 { get; set; }
    }
}
