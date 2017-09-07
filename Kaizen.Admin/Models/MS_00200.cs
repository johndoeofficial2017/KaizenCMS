using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class MS_00200
    {
        public MS_00200()
        {
            this.MS_00201 = new List<MS_00201>();
        }

        public string TrxID { get; set; }
        public int MsgTemplateID { get; set; }
        public string MsgTemplateContant { get; set; }
        public string MsgTemplateName { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public System.DateTime EntryDate { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public virtual MS_00002 MS_00002 { get; set; }
        public virtual ICollection<MS_00201> MS_00201 { get; set; }
    }
}
