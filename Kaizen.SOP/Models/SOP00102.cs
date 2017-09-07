using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00102
    {
        public string CUSTNMBR { get; set; }
        public int AccountsReceivable { get; set; }
        public Nullable<int> Sales { get; set; }
        public Nullable<int> CostofGoodsSold { get; set; }
        public Nullable<int> Inventory { get; set; }
        public Nullable<int> TermsDiscountAvailable { get; set; }
        public Nullable<int> TermsDiscountTaken { get; set; }
        public Nullable<int> FinanceCharges { get; set; }
        public Nullable<int> Writeoffs { get; set; }
        public Nullable<int> OverpaymentWriteoffs { get; set; }
        public Nullable<int> SalesOrderReturns { get; set; }
        public Nullable<int> Cash { get; set; }
        public virtual SOP00100 SOP00100 { get; set; }
    }
}
