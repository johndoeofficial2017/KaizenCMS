using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00036
    {
        public CM00036()
        {
            this.CM00234 = new List<CM00234>();
        }

        public int TemplateID { get; set; }
        public int TRXTypeID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateContant { get; set; }
        public virtual ICollection<CM00234> CM00234 { get; set; }
    }
}
