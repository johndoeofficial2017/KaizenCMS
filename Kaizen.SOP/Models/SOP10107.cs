using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10107
    {
        public int DocumentID { get; set; }
        public string SOPNUMBE { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual SOP10100 SOP10100 { get; set; }
    }
}
