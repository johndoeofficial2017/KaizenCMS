using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class Sys00020
    {
        public Sys00020()
        {
            this.SOP00500 = new List<SOP00500>();
        }

        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public virtual ICollection<SOP00500> SOP00500 { get; set; }
    }
}
