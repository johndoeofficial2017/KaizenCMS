using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class DT00001
    {
        public DT00001()
        {
            this.DT00100 = new List<DT00100>();
        }

        public int ViewType { get; set; }
        public string ViewName { get; set; }
        public virtual ICollection<DT00100> DT00100 { get; set; }
    }
}
