using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class KaizenGridViewAccess
    {
        public int ViewID { get; set; }
        public string UserName { get; set; }
        public bool IsDefault { get; set; }
        public virtual Kaizen00011 Kaizen00011 { get; set; }
    }
}
