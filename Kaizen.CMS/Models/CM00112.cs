using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00112
    {
        public int DocumentID { get; set; }
        public string ClientID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual CM00110 CM00110 { get; set; }
    }
}
