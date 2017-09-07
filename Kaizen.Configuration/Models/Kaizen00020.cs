using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00020
    {
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string EmailIPassword { get; set; }
        public string IncomingProtocal { get; set; }
        public string OutGoingProtocal { get; set; }
        public Nullable<bool> IsSSL { get; set; }
        public Nullable<int> InComingPort { get; set; }
        public Nullable<int> OutGoingPort { get; set; }
        public string EmailTitle { get; set; }
        public virtual User User { get; set; }
    }
}
