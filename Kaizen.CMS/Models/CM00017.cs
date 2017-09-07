using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00017
    {
        public CM00017()
        {
            this.CM00203 = new List<CM00203>();
        }

        public int Lookup02 { get; set; }
        public string Lookup02Name { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
