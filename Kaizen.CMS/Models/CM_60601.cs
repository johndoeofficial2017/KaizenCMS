using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_60601
    {
        public string ClientID { get; set; }
        public string ContractID { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public string PeriodName { get; set; }
        public int BucketID { get; set; }
        public Nullable<double> OutStandingAMT { get; set; }
        public Nullable<int> DebtorCount { get; set; }
        public Nullable<int> CaseCount { get; set; }
    }
}
