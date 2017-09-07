using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen006
    {
        public int RoleID { get; set; }
        public string CompanyID { get; set; }
        public string CheckbookCode { get; set; }
        public string CurrencyCode { get; set; }
        public byte DecimalDigit { get; set; }
        public bool IsMultiply { get; set; }
        public double ExchangeRate { get; set; }
        public virtual Kaizen00030 Kaizen00030 { get; set; }
    }
}
