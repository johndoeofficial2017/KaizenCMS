using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00033
    {
        public CM00033()
        {
            this.CM00131 = new List<CM00131>();
        }

        public int AddressCodeType { get; set; }
        public string AddressTypeName { get; set; }
        public virtual ICollection<CM00131> CM00131 { get; set; }
    }
}
