using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00005
    {
        public Sys00005()
        {
            this.Sys00006 = new List<Sys00006>();
            this.Sys00007 = new List<Sys00007>();
        }

        public int ContactSourceID { get; set; }
        public string ContactSourceName { get; set; }
        public virtual ICollection<Sys00006> Sys00006 { get; set; }
        public virtual ICollection<Sys00007> Sys00007 { get; set; }
    }
}
