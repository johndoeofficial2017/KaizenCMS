using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10252
    {
        public int DocumentID { get; set; }
        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual SOP10250 SOP10250 { get; set; }
    }
}
