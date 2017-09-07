using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_CaseNewForUploading
    {
        public string  CaseRef { get; set; }
        public string DebtorID { get; set; }
        public Nullable<int> BucketID { get; set; }
        public string CaseAddess { get; set; }
        public string CaseAccountNo { get; set; }
        public string InvoiceNumber { get; set; }
        public string Remarks { get; set; }
        public Nullable<double> OSAmount { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public Nullable<double> PrincipleAmount { get; set; }
        public string AgentID { get; set; }
        public string AssignDate { get; set; }
        public string LastPaymentDate { get; set; }
        public Nullable<double> LastCallactedAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public string LastPaymentBy { get; set; }
        public string UserName { get; set; }
    }
}
