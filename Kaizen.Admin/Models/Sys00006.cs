using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00006
    {
        public Sys00006()
        {
        }

        public int ContactTypeID { get; set; }
        public string ContactTypeName { get; set; }
        public int ContactSourceID { get; set; }
        public virtual Sys00005 Sys00005 { get; set; }
    }
}
