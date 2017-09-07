using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM30204
    {
        public double Amount { get; set; }
        public string PaymentID { get; set; }
        public string CaseRef { get; set; }
        public virtual CM30207 CM30207 { get; set; }
    }
}
