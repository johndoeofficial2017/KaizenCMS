using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10203
    {
        public int DocumentID { get; set; }
        public int LineID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string PhotoExtension { get; set; }
        public virtual SOP10201 SOP10201 { get; set; }
    }
}
