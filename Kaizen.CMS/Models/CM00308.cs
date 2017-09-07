using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00308
    {
        public string PaymentID { get; set; }
        public string CaseRef { get; set; }
        public double Amount { get; set; }
        public virtual CM00307 CM00307 { get; set; }
    }
}
