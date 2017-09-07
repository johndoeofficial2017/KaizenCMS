using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_600200
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public int CaseStatusID { get; set; }
        public string CaseStatusname { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<double> TotalClaimAmount { get; set; }
        public Nullable<int> CaseCount { get; set; }
    }
}
