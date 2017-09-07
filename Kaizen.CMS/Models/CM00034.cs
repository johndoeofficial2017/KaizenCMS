using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00034
    {
        public CM00034()
        {
            this.CM00141 = new List<CM00141>();
        }

        public int AddressCodeType { get; set; }
        public string AddressTypeName { get; set; }
        public virtual ICollection<CM00141> CM00141 { get; set; }
    }
}
