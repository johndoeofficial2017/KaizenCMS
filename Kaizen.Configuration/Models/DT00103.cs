using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class DT00103
    {
        public int SourceID { get; set; }
        public string UserName { get; set; }
        public bool IsDefault { get; set; }
        public virtual DT00100 DT00100 { get; set; }
    }
}
