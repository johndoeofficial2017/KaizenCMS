using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10510
    {
        public SOP10510()
        {
            this.SOP10500 = new List<SOP10500>();
        }

        public string BatchID { get; set; }
        public string BatchName { get; set; }
        public virtual ICollection<SOP10500> SOP10500 { get; set; }
    }
}
