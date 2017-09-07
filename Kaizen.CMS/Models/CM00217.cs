using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00217
    {
        public string ClientID { get; set; }
        public string ContractID { get; set; }
        public string AgentID { get; set; }
        public int PERIODID { get; set; }
        public int BucketID { get; set; }
        public string ClientName { get; set; }
        public string ContractName { get; set; }
        public string BucketName { get; set; }
        public string PeriodName { get; set; }
        public Nullable<double> TargetAmount { get; set; }
        public double OutStandingAMT { get; set; }
        public Nullable<double> CollectedAmount { get; set; }
        public double DebtorCount { get; set; }
        public double CaseCount { get; set; }
        public Nullable<double> OutStandingAMTPost { get; set; }
        public Nullable<double> CollectedAmountPost { get; set; }
        public Nullable<double> DebtorCountPost { get; set; }
        public Nullable<double> CaseCountPost { get; set; }
    }
}
