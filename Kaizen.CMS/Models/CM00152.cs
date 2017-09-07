using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00152
    {
        public string UserName { get; set; }
        public string BatchID { get; set; }
        public int TRXTypeID { get; set; }
        public string TrxTypeName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string NationaltyClient { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string CurrencyCode { get; set; }
        public byte DecimalDigit { get; set; }
        public bool IsMultiply { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public Nullable<int> CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public string AddressCode { get; set; }
        public string FileName { get; set; }
    }
}
