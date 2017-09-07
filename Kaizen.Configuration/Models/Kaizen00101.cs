using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00101
    {
        public Kaizen00101()
        {
            this.Kaizen004 = new List<Kaizen004>();
        }

        public string CompanyID { get; set; }
        public int ModuleID { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Kaizen004> Kaizen004 { get; set; }
    }
}
