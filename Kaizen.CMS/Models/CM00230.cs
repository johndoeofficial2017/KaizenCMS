using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00230
    {
        public CM00230()
        {
            this.CM00231 = new List<CM00231>();
        }

        public string MessageTRXID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string TrxComment { get; set; }
        public int TemplateID { get; set; }
        public string TemplateContant { get; set; }
        public string AddressCode { get; set; }
        public string EmailID { get; set; }
        public string EmailIPassword { get; set; }
        public string IncomingProtocal { get; set; }
        public string OutGoingProtocal { get; set; }
        public Nullable<bool> IsSSL { get; set; }
        public Nullable<int> InComingPort { get; set; }
        public Nullable<int> OutGoingPort { get; set; }
        public string EmailTitle { get; set; }
        public Nullable<bool> Email01 { get; set; }
        public Nullable<bool> Email02 { get; set; }
        public Nullable<bool> Email03 { get; set; }
        public Nullable<bool> Email04 { get; set; }
        public virtual CM00030 CM00030 { get; set; }
        public virtual ICollection<CM00231> CM00231 { get; set; }
    }
}
