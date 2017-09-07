using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00007
    {
        public Sys00007()
        {
        }

        public int DocumentTypeID { get; set; }
        public int ContactSourceID { get; set; }
        public string DocumentTypeName { get; set; }
        public virtual Sys00005 Sys00005 { get; set; }
    }
}
