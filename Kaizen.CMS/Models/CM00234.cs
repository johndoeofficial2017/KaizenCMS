using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00234
    {
        public CM00234()
        {
            this.CM00235 = new List<CM00235>();
        }

        public string MessageTRXID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string TrxComment { get; set; }
        public int TemplateID { get; set; }
        public string TemplateContant { get; set; }
        public string AddressCode { get; set; }
        public virtual CM00036 CM00036 { get; set; }
        public virtual ICollection<CM00235> CM00235 { get; set; }
    }
}
