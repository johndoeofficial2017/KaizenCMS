using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_CaseOld
    {
        public string DebtorID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string CaseAddess { get; set; }
        public string CaseAccountNo { get; set; }
        public string InvoiceNumber { get; set; }
        public string Remarks { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
    }
}
