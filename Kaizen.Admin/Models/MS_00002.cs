using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class MS_00002
    {
        public MS_00002()
        {
            this.MS_00200 = new List<MS_00200>();
            this.MS_00003 = new List<MS_00003>();
        }

        public int MsgTemplateID { get; set; }
        public string MsgTemplateName { get; set; }
        public string MsgTemplateContant { get; set; }
        public virtual ICollection<MS_00200> MS_00200 { get; set; }
        public virtual ICollection<MS_00003> MS_00003 { get; set; }
    }
}
