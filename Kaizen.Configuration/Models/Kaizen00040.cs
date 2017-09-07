using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00040
    {
        public string SMSAccountCode { get; set; }
        public string CompanyID { get; set; }
        public string SMSAccountName { get; set; }
        public virtual Company Company { get; set; }
    }
}
