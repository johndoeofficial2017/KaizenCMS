using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class KaizenSession
    {
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public System.Guid KaizenPublicKey { get; set; }
        public System.DateTime LoginDate { get; set; }
        public Nullable<System.DateTime> LoginDateOut { get; set; }
        public virtual User User { get; set; }
    }
}
