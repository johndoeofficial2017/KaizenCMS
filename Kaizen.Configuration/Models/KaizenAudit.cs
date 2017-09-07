using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class KaizenAudit
    {
        public System.DateTime Kaizen_DATE { get; set; }
        public string Kaizen_USER { get; set; }
        public string Kaizen_HOST { get; set; }
        public string Kaizen_DB { get; set; }
        public string Kaizen_Table { get; set; }
        public string Ins { get; set; }
        public string Del { get; set; }
    }
}
