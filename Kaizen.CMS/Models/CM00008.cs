using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00008
    {
        public CM00008()
        {
            this.CM00111 = new List<CM00111>();
        }

        public int AddressCodeType { get; set; }
        public string AddressTypeName { get; set; }
        public virtual ICollection<CM00111> CM00111 { get; set; }
    }
}
