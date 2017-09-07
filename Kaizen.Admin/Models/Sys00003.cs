using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00003
    {
        public Sys00003()
        {
            this.Sys00004 = new List<Sys00004>();
        }

        public byte ModuleID { get; set; }
        public string ModuleName { get; set; }
        public virtual ICollection<Sys00004> Sys00004 { get; set; }
    }
}
