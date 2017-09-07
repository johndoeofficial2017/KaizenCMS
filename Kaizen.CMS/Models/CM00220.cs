using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00220
    {
        public string TrxID { get; set; }
        public string AgentID { get; set; }
        public string AgentIDTO { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string UserName { get; set; }
        public string TrxComments { get; set; }
        public virtual CM00105 CM00105 { get; set; }
    }
}
