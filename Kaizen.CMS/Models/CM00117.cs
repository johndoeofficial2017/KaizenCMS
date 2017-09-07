using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00117
    {
        public int CommissionID { get; set; }
        public string YearCode { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public string TargetID { get; set; }
        public Nullable<double> TargetFrom { get; set; }
        public Nullable<double> TargetTo { get; set; }
        public Nullable<double> CommissionAMT { get; set; }
        public Nullable<double> CommissionPercent { get; set; }
        public virtual CM00107 CM00107 { get; set; }
    }
}
