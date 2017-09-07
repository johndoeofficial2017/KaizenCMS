using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class CRM00202
    {
        public int ReceiverID { get; set; }
        public int TransactionID { get; set; }
        public string ReceiverEmail { get; set; }
        public virtual CRM00200 CRM00200 { get; set; }
    }
}
