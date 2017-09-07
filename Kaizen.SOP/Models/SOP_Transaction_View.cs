using System;

namespace Kaizen.SOP
{
    public partial class SOP_Transaction_View
    {
        public string TrxTypeName { get; set; }
        public string DocNumber { get; set; }
        public string BatchID { get; set; }
        public byte TRXTypeID { get; set; }
        public string CUSTNMBR { get; set; }
        public string AddressCode { get; set; }
        public string CUSTNAME { get; set; }
        public string DocDescription { get; set; }
        public string Expr1 { get; set; }
        public decimal AMT { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> TradeDiscount { get; set; }
        public Nullable<decimal> Markdown { get; set; }
        public Nullable<decimal> Cash { get; set; }
        public Nullable<decimal> Miscellaneous { get; set; }
        public Nullable<decimal> CheckPay { get; set; }
        public Nullable<decimal> CreditCard { get; set; }
        public Nullable<decimal> TermsDiscountTaken { get; set; }
        public int DEX_ROW_ID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string ISOCode { get; set; }
        public string CurrencySymbol { get; set; }
        public byte DecimalDigit { get; set; }
        public string DecimalSeparator { get; set; }
        public string GroupSeparator { get; set; }
        public byte GroupSizes { get; set; }
    }
}
