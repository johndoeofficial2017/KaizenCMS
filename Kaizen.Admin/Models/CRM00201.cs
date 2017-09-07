using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class CRM00201
    {
        public System.Guid DocumentID { get; set; }
        public int TransactionID { get; set; }
        public string DocumentName { get; set; }
        public string PhotoExtension { get; set; }
        public virtual CRM00200 CRM00200 { get; set; }
    }
}
