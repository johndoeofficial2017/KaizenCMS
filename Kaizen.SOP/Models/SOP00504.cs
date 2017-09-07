using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00504
    {
        public int DocumentID { get; set; }
        public string TrxDocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual SOP00500 SOP00500 { get; set; }
    }
}
