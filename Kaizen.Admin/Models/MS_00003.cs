using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class MS_00003
    {
        public string DocumentID { get; set; }
        public int MsgTemplateID { get; set; }
        public virtual MS_00002 MS_00002 { get; set; }
    }
}
