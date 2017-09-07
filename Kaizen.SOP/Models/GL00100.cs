using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class GL00100
    {
        public GL00100()
        {
            this.SOP10102 = new List<SOP10102>();
        }

        public int AccountID { get; set; }
        public string ACTNUMBR { get; set; }
        public int CategoryID { get; set; }
        public string AccountName { get; set; }
        public string AccountAlias { get; set; }
        public bool IsPL { get; set; }
        public bool IsDebit { get; set; }
        public bool InSales { get; set; }
        public bool InPurchase { get; set; }
        public bool InInventory { get; set; }
        public bool InPayroll { get; set; }
        public bool InManufacturing { get; set; }
        public bool InExpenseManagement { get; set; }
        public bool InPOS { get; set; }
        public bool Inbank { get; set; }
        public bool IsAllowAccountEntry { get; set; }
        public bool Inactive { get; set; }
        public string ACTNUMBR_1 { get; set; }
        public string ACTNUMBR_2 { get; set; }
        public string ACTNUMBR_3 { get; set; }
        public string ACTNUMBR_4 { get; set; }
        public string ACTNUMBR_5 { get; set; }
        public string ACTNUMBR_6 { get; set; }
        public string ACTNUMBR_7 { get; set; }
        public string ACTNUMBR_8 { get; set; }
        public string ACTNUMBR_9 { get; set; }
        public string ACTNUMBR_10 { get; set; }
        public string ACTNUMBR_16 { get; set; }
        public string ACTNUMBR_12 { get; set; }
        public string ACTNUMBR_13 { get; set; }
        public string ACTNUMBR_14 { get; set; }
        public string ACTNUMBR_15 { get; set; }
        public virtual ICollection<SOP10102> SOP10102 { get; set; }
    }
}
