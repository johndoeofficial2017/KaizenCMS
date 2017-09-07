using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00103
    {
        public string CUSTNMBR { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<bool> FreightType { get; set; }
        public Nullable<double> FreightAmt { get; set; }
        public Nullable<bool> TradeDiscountType { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public Nullable<short> FinanceChargeType { get; set; }
        public Nullable<double> FinanceChargeAmt { get; set; }
        public Nullable<short> MinimumPaymentType { get; set; }
        public Nullable<double> MinimumPaymentAMT { get; set; }
        public Nullable<short> CreditLimitType { get; set; }
        public Nullable<double> CreditLimitAMT { get; set; }
        public Nullable<short> MaximumWriteoffType { get; set; }
        public Nullable<double> MaximumWriteoffAMT { get; set; }
        public virtual SOP00100 SOP00100 { get; set; }
    }
}
