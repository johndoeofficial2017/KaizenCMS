using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00020
    {
        public Sys00020()
        {
        }

        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public string ReceiptPrefix { get; set; }
        public Nullable<int> ReceiptLengh { get; set; }
        public Nullable<int> ReceiptLast { get; set; }
    }
}
