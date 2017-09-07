using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00015
    {
        public SOP00015()
        {
            this.SOP10100 = new List<SOP10100>();
            this.SOP10200 = new List<SOP10200>();
            this.SOP10250 = new List<SOP10250>();
            this.SOP10500 = new List<SOP10500>();
        }

        public string PaymentTermID { get; set; }
        public string PaymentTermName { get; set; }
        public virtual ICollection<SOP10100> SOP10100 { get; set; }
        public virtual ICollection<SOP10200> SOP10200 { get; set; }
        public virtual ICollection<SOP10250> SOP10250 { get; set; }
        public virtual ICollection<SOP10500> SOP10500 { get; set; }
    }
}
