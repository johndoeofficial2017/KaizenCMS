using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00028
    {
        public CM00028()
        {
            this.CM00024 = new List<CM00024>();
            this.CM00060 = new List<CM00060>();
            this.CM00070 = new List<CM00070>();
        }

        public short FieldTypeID { get; set; }
        public string FieldTypeName { get; set; }
        public virtual ICollection<CM00024> CM00024 { get; set; }
        public virtual ICollection<CM00060> CM00060 { get; set; }
        public virtual ICollection<CM00070> CM00070 { get; set; }
    }
}
