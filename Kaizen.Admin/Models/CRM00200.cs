using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class CRM00200
    {
        public CRM00200()
        {
            this.CRM00201 = new List<CRM00201>();
            this.CRM00202 = new List<CRM00202>();
        }

        public int TransactionID { get; set; }
        public string TransactionIName { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string TransactionMessage { get; set; }
        public virtual ICollection<CRM00201> CRM00201 { get; set; }
        public virtual ICollection<CRM00202> CRM00202 { get; set; }
    }
}
