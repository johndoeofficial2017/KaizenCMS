using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00400
    {
        public int KaizenLineID { get; set; }
        public string KaizenMessageLine { get; set; }
        public string UserNameTo { get; set; }
        public string UserName { get; set; }
        public System.DateTime MessageDate { get; set; }
        public bool IsReceived { get; set; }
        public virtual User User { get; set; }
    }
}
