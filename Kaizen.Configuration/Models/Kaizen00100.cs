using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00100
    {
        public int TransactionID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public bool IsPhoneIN { get; set; }
    }
}
