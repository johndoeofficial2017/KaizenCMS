using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00106
    {
        public int DocumentID { get; set; }
        public string CUSTNMBR { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual SOP00100 SOP00100 { get; set; }
        public virtual Sys00007 Sys00007 { get; set; }
    }
}
