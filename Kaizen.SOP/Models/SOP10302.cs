using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10302
    {
        public int LineID { get; set; }
        public string SOPNUMBE { get; set; }
        public double DOCAMNT { get; set; }
        public string CreditCardNumber { get; set; }
        public string BankName { get; set; }
        public virtual SOP10300 SOP10300 { get; set; }
    }
}
