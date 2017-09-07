using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00120
    {
        public string ClientID { get; set; }
        public int PERIODID { get; set; }
        public double ClaimAmount { get; set; }
        public double TotalWriteOff { get; set; }
        public double FinanceCharge { get; set; }
        public double TotalCallactedAMT { get; set; }
        public double TotalCaseNumber { get; set; }
        public virtual CM00007 CM00007 { get; set; }
        public virtual CM00110 CM00110 { get; set; }
    }
}
