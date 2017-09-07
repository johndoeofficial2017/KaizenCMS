using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM30230
    {
        public string YearCode { get; set; }
        public int PERIODID { get; set; }
        public string PeriodName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public double CaseCount { get; set; }
        public double OutstandingAMT { get; set; }
        public double TotalCallactedAMT { get; set; }
    }
}
