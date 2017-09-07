using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00103
    {
        public System.Guid DocumentID { get; set; }
        public int TaskID { get; set; }
        public int DocTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual Sys00100 Sys00100 { get; set; }
        public virtual Sys00104 Sys00104 { get; set; }
    }
}
