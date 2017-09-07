using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00055
    {
        public CM00055()
        {
            this.CM00203 = new List<CM00203>();
        }

        public int ProductID { get; set; }
        public int TRXTypeID { get; set; }
        public string ProductName { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
