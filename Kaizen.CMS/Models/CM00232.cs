using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00232
    {
        public CM00232()
        {
            this.CM00233 = new List<CM00233>();
        }

        public string MessageTRXID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string TrxComment { get; set; }
        public int TemplateID { get; set; }
        public string TemplateContant { get; set; }
        public string AddressCode { get; set; }
        public string SMSAccountCode { get; set; }
        public Nullable<bool> MobileNo1 { get; set; }
        public Nullable<bool> MobileNo2 { get; set; }
        public Nullable<bool> MobileNo3 { get; set; }
        public Nullable<bool> MobileNo4 { get; set; }
        public virtual CM00035 CM00035 { get; set; }
        public virtual ICollection<CM00233> CM00233 { get; set; }
    }
}
