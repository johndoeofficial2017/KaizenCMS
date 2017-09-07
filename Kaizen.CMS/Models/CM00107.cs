using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00107
    {
        public CM00107()
        {
            this.CM00117 = new List<CM00117>();
        }

        public string YearCode { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public string TargetID { get; set; }
        public string PeriodName { get; set; }
        public bool IsClosed { get; set; }
        public bool IsPercentTarget { get; set; }
        public int TargetTypeID { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public Nullable<double> CaseCount { get; set; }
        public Nullable<double> DebtorCount { get; set; }
        public Nullable<double> TargetClaimAmount { get; set; }
        public Nullable<double> TargetCaseCount { get; set; }
        public Nullable<double> TargetDebtorCount { get; set; }
        public Nullable<double> CollectedAmount { get; set; }
        public Nullable<double> CollectedCaseCount { get; set; }
        public Nullable<double> CollectedDebtorCount { get; set; }
        public virtual CM00109 CM00109 { get; set; }
        public virtual CM10110 CM10110 { get; set; }
        public virtual ICollection<CM00117> CM00117 { get; set; }
    }
}
