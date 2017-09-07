using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00209
    {
        public CM00209()
        {
            this.CM00210 = new List<CM00210>();
        }

        public string TransactionID { get; set; }
        public string ClientID { get; set; }
        public string ContractID { get; set; }
        public string DebtorID { get; set; }
        public Nullable<int> ActionPlanID { get; set; }
        public string EmployeeID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<CM00210> CM00210 { get; set; }
    }
}
