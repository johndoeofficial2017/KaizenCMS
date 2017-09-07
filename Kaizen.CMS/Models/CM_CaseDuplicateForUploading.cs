using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_CaseDuplicateForUploading
    {
        public int CaseRef { get; set; }
        public string DebtorID { get; set; }
        public string CaseAddess { get; set; }
        public string CaseAccountNo { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
    }
}
