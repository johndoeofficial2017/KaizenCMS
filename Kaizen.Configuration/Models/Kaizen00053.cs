using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00053
    {
        public int ReportID { get; set; }
        public int DashboardCode { get; set; }
        public bool IsDefault { get; set; }
        public virtual Kaizen00050 Kaizen00050 { get; set; }
        public virtual Kaizen00052 Kaizen00052 { get; set; }
    }
}
