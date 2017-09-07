using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class MS_00001
    {
        public MS_00001()
        {
            this.CRM00200 = new List<CRM00200>();
        }

        public int MsgSourceID { get; set; }
        public string MsgSourceName { get; set; }
        public virtual ICollection<CRM00200> CRM00200 { get; set; }
    }
}
