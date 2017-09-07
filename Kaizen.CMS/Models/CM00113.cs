using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00113
    {
        public string ClientID { get; set; }
        public int EFTTypeID { get; set; }
        public string BankCode { get; set; }
        public string BankAccount { get; set; }
        public string IBANNumber { get; set; }
        public string OtherInfo { get; set; }
        public virtual CM00110 CM00110 { get; set; }
    }
}
