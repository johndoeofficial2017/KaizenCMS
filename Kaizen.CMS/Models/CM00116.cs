using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00116
    {
        public int CommissionID { get; set; }
        public string YearCode { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public Nullable<double> TargetFrom { get; set; }
        public Nullable<double> TargetTo { get; set; }
        public Nullable<double> CommissionAMT { get; set; }
        public Nullable<double> CommissionPercent { get; set; }
        public virtual CM10110 CM10110 { get; set; }
    }
}
