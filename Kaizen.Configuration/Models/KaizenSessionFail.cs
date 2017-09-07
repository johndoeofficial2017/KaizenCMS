using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class KaizenSessionFail
    {
        public int LineID { get; set; }
        public string UserName { get; set; }
        public System.DateTime LoginDate { get; set; }
        public virtual User User { get; set; }
    }
}
