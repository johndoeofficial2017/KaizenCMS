using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00016
    {
        public CM00016()
        {
            this.CM00203 = new List<CM00203>();
        }

        public int Lookup01 { get; set; }
        public string Lookup01Name { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
