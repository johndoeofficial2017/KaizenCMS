using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00212
    {
        public string YearCode { get; set; }
        public string ClientID { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public string ClientName { get; set; }
        public string PeriodName { get; set; }
        public Nullable<double> TargetAmount { get; set; }
        public Nullable<double> OutStandingAMT { get; set; }
        public Nullable<double> CollectedAmount { get; set; }
        public Nullable<double> DebtorCount { get; set; }
        public Nullable<double> CaseCount { get; set; }
        public Nullable<double> OutStandingAMTPost { get; set; }
        public Nullable<double> CollectedAmountPost { get; set; }
        public Nullable<double> DebtorCountPost { get; set; }
        public Nullable<double> CaseCountPost { get; set; }
        public virtual CM00110 CM00110 { get; set; }
        public virtual CM10110 CM10110 { get; set; }
    }
}
