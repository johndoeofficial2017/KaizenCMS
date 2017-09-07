using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_60610
    {
        public string AgentID { get; set; }
        public Nullable<double> TargetClaimAmount { get; set; }
        public Nullable<double> CollectedAmount { get; set; }
        public Nullable<double> DebtorCount { get; set; }
        public Nullable<double> CaseCount { get; set; }
        public int CurrentPeriod { get; set; }
        public Nullable<int> PreviousValue { get; set; }
        public Nullable<double> PreviousTargetAmount { get; set; }
        public Nullable<double> PreviousCollectedAmount { get; set; }
        public Nullable<double> PreviousDebtorCount { get; set; }
        public Nullable<double> PreviousCaseCount { get; set; }
    }
}
