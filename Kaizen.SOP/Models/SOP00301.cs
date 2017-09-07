using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00301
    {
        public SOP00301()
        {
            this.SOP10300 = new List<SOP10300>();
        }

        public string SOPTypeSetupID { get; set; }
        public int CategoryID { get; set; }
        public string SOPTypeSetupName { get; set; }
        public virtual SOP00300 SOP00300 { get; set; }
        public virtual ICollection<SOP10300> SOP10300 { get; set; }
    }
}
