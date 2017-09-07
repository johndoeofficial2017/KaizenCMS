using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00203View02
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public int CaseStatusID { get; set; }
        public string CaseStatusname { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public Nullable<int> CaseVolume { get; set; }
    }
}
