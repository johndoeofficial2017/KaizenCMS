using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00170
    {
        public CM00170()
        {
            this.CM00203 = new List<CM00203>();
        }

        public string CreditorID { get; set; }
        public string CreditorName { get; set; }
        public string ShortName { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
