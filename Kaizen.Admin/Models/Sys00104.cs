using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00104
    {
        public Sys00104()
        {
            this.Sys00103 = new List<Sys00103>();
        }

        public int DocTypeID { get; set; }
        public string DocTypeName { get; set; }
        public virtual ICollection<Sys00103> Sys00103 { get; set; }
    }
}
