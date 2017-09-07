using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10110
    {
        public CM10110()
        {
            this.CM00107 = new List<CM00107>();
            this.CM00116 = new List<CM00116>();
        }

        public string YearCode { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
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
        public virtual CM00105 CM00105 { get; set; }
        public virtual ICollection<CM00107> CM00107 { get; set; }
        public virtual ICollection<CM00116> CM00116 { get; set; }
    }
}
