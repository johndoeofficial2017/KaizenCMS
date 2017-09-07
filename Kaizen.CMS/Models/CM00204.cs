using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00204
    {
        public string PaymentID { get; set; }
        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public virtual CM00203 CM00203 { get; set; }
        public virtual CM00207 CM00207 { get; set; }
    }
}
