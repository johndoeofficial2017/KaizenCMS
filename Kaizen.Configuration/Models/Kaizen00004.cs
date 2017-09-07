using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00004
    {
        public Kaizen00004()
        {
            this.Kaizen00005 = new List<Kaizen00005>();
        }

        public int TaskSourceID { get; set; }
        public string TaskSourceName { get; set; }
        public virtual ICollection<Kaizen00005> Kaizen00005 { get; set; }
    }
}
