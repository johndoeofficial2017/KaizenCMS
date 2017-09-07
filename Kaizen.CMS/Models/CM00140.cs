using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00140
    {
        public CM00140()
        {
            this.CM00141 = new List<CM00141>();
            this.CM00203 = new List<CM00203>();
        }

        public string LegalID { get; set; }
        public string LegalName { get; set; }
        public string ShortName { get; set; }
        public string PartnerDescription { get; set; }
        public string LegalClassID { get; set; }
        public string StatusID { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public Nullable<double> FinanceCharge { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<double> TotalCaseNumber { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<int> AddressCode { get; set; }
        public Nullable<int> BillAddressCode { get; set; }
        public virtual CM00022 CM00022 { get; set; }
        public virtual CM00066 CM00066 { get; set; }
        public virtual ICollection<CM00141> CM00141 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
