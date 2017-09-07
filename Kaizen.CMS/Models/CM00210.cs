using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00210
    {
        public string CaseRef { get; set; }
        public string TransactionID { get; set; }
        public string JecketsID { get; set; }
        public string ProductID { get; set; }
        public Nullable<int> CaseTreeID { get; set; }
        public string CaseAddess { get; set; }
        public string CaseContractDetail { get; set; }
        public Nullable<int> AddressCode { get; set; }
        public string CaseAccountNo { get; set; }
        public System.DateTime ClosingDate { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public System.DateTime BookingDate { get; set; }
        public int CaseStatusID { get; set; }
        public string Remarks { get; set; }
        public Nullable<double> OriginalAmount { get; set; }
        public double ClaimAmount { get; set; }
        public double PrincipleAmount { get; set; }
        public virtual CM00209 CM00209 { get; set; }
    }
}
