using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00035
    {
        public CM00035()
        {
            this.CM00232 = new List<CM00232>();
        }

        public int TemplateID { get; set; }
        public int TRXTypeID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateContant { get; set; }
        public virtual ICollection<CM00232> CM00232 { get; set; }
    }
}
