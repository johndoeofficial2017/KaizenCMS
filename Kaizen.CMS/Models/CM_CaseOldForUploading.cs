using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_CaseOldForUploading
    {
        public string CaseRef { get; set; }
        public string DebtorID { get; set; }
        public string DebtorName { get; set; }
        public string CaseAccountNo { get; set; }
        public string Remarks { get; set; }
        public double ClaimAmount { get; set; }
        public string AgentID { get; set; }
        public string CaseStatusname { get; set; }
        public string ClientID { get; set; }
        public Nullable<System.Guid> KaizenPublicKey { get; set; }
    }
}
