using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00088
    {
        public CM00088()
        {
            this.CM00089 = new List<CM00089>();
        }

        public int TrxPaymentType { get; set; }
        public string PaymentTypeName { get; set; }
        public virtual ICollection<CM00089> CM00089 { get; set; }
    }
}
