using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00211
    {
        public int ActionID { get; set; }
        public System.DateTime ActionDate { get; set; }
        public string CaseRef { get; set; }
        public int ActionTypeID { get; set; }
        public string Remarks { get; set; }
        public string AgentID { get; set; }
        public string NextActionTypeID { get; set; }
        public Nullable<System.DateTime> NextActionDate { get; set; }
        public Nullable<double> NextActionAMT { get; set; }
        public virtual CM00203 CM00203 { get; set; }
    }
}
