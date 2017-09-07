using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00200
    {
        public CM00200()
        {
            this.CM10100 = new List<CM10100>();
        }

        public string ContractID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ContractName { get; set; }
        public int ContractStatusID { get; set; }
        public Nullable<int> PaymentBaseID { get; set; }
        public Nullable<int> BillingFrequencyID { get; set; }
        public string Abbreviation { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string BilltoCustomer { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<System.DateTime> LastPaymentDate { get; set; }
        public Nullable<double> TargetAmount { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<bool> IsPrivateCase { get; set; }
        public string Remarks { get; set; }
        public virtual CM00013 CM00013 { get; set; }
        public virtual CM00018 CM00018 { get; set; }
        public virtual CM00019 CM00019 { get; set; }
        public virtual CM00110 CM00110 { get; set; }
        public virtual ICollection<CM10100> CM10100 { get; set; }
    }
}
