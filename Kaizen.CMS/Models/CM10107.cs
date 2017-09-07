using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10107
    {
        public int LineID { get; set; }
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public string AgentID { get; set; }
        public string CIFNumber { get; set; }
        public string TargetID { get; set; }
        public double ClaimAmount { get; set; }
        public Nullable<double> OutstandingAMT { get; set; }
    }
}
