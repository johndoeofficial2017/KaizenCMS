using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class CompanyAccess
    {
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public Nullable<int> YearCode { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public string ExchangeTableID { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<int> ExchangeRateID { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }
        public string CheckbookCode { get; set; }
        public string SOPTypeSetupID { get; set; }
        public Nullable<byte> SOPTYPE { get; set; }
        public string SiteID { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public Nullable<int> DashboardCode { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
