using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class KaizenSequence
    {
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public string SequenceName { get; set; }
        public int KaizenID { get; set; }
        public virtual Company Company { get; set; }
    }
}
