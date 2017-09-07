using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00030
    {
        public CM00030()
        {
            this.CM00230 = new List<CM00230>();
        }

        public int TemplateID { get; set; }
        public int TRXTypeID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateContant { get; set; }
        public string TemplateTags { get; set; }
        public virtual ICollection<CM00230> CM00230 { get; set; }
    }
}
