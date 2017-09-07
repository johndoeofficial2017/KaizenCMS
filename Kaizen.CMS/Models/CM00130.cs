using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00130
    {
        public CM00130()
        {
            this.CM00131 = new List<CM00131>();
            this.CM00203 = new List<CM00203>();
        }

        public string PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string ShortName { get; set; }
        public string PartnerDescription { get; set; }
        public string PartnerClassID { get; set; }
        public string StatusID { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public Nullable<double> FinanceCharge { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<double> TotalCaseNumber { get; set; }
        public string PhotoExtension { get; set; }
        public string ParentClientID { get; set; }
        public Nullable<int> AddressCode { get; set; }
        public Nullable<int> BillAddressCode { get; set; }
        public virtual CM00002 CM00002 { get; set; }
        public virtual CM00022 CM00022 { get; set; }
        public virtual ICollection<CM00131> CM00131 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
