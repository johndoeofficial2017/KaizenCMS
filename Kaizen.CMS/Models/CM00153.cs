using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00153
    {
        public string UserName { get; set; }
        public string ClientID { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public int BucketID { get; set; }
        public string PeriodName { get; set; }
        public Nullable<double> OutStandingAMT { get; set; }
        public Nullable<double> CollectedAmount { get; set; }
        public Nullable<double> DebtorCount { get; set; }
        public Nullable<double> CaseCount { get; set; }
    }
}
