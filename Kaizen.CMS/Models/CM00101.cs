using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00101
    {
        public int DocumentID { get; set; }
        public string DebtorID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual CM00100 CM00100 { get; set; }
    }
}
