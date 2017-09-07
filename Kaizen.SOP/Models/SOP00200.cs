using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00200
    {
        public string DocNumber { get; set; }
        public Nullable<int> BatchID { get; set; }
        public string TermCode { get; set; }
        public byte TRXTypeID { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public string AddressCode { get; set; }
        public string EmployeeID { get; set; }
        public string TerritoryID { get; set; }
        public string ShippingID { get; set; }
        public string DocDescription { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public string ExchangeTableID { get; set; }
        public Nullable<int> ExchangeRateID { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public string PONumber { get; set; }
        public double AMT { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<double> TradeDiscount { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<double> Markdown { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<short> PaymentMethod { get; set; }
        public Nullable<double> PaymentAMT { get; set; }
        public Nullable<bool> IsDiscountPercent { get; set; }
        public Nullable<double> TermsDiscountTaken { get; set; }
        public string PaymentNumber { get; set; }
        public Nullable<int> KaizenID { get; set; }
        public virtual SOP00020 SOP00020 { get; set; }
        public virtual SOP00022 SOP00022 { get; set; }
        public virtual SOP00023 SOP00023 { get; set; }
        public virtual SOP00100 SOP00100 { get; set; }
        public virtual SOP00201 SOP00201 { get; set; }
    }
}
