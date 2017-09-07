using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00309
    {
        public CM00309()
        {
            this.CM00307 = new List<CM00307>();
        }

        public int ReasonID { get; set; }
        public string ReasonName { get; set; }
        public virtual ICollection<CM00307> CM00307 { get; set; }
    }
}
