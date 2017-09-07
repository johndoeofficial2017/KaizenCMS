using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00006
    {
        public Met00006()
        {
            this.Met00100 = new List<Met00100>();
        }

        public int RepeatTypeID { get; set; }
        public string RepeatUnit { get; set; }
        public string RepeatTypeName { get; set; }
        public virtual ICollection<Met00100> Met00100 { get; set; }
    }
}
