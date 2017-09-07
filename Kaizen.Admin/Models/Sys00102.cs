using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00102
    {
        public System.Guid DocumentID { get; set; }
        public int ActionID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual Sys00101 Sys00101 { get; set; }
    }
}
