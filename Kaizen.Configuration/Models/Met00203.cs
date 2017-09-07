using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Met00203
    {
        public int MeetingID { get; set; }
        public string CaseRef { get; set; }
        public virtual Met00200 Met00200 { get; set; }
    }
}
