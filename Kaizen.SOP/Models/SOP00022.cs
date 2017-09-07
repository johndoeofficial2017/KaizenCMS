using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00022
    {
        public SOP00022()
        {
            this.SOP00200 = new List<SOP00200>();
        }

        public string TermCode { get; set; }
        public byte DueType { get; set; }
        public int Due { get; set; }
        public byte DiscountType { get; set; }
        public int Discount { get; set; }
        public bool IsPercent { get; set; }
        public decimal DiscountValue { get; set; }
        public Nullable<bool> FinanceChrgesIsPercent { get; set; }
        public decimal FinanceChargeValue { get; set; }
        public bool ApplyTo { get; set; }
        public bool ApplyDiscount { get; set; }
        public bool ApplyFreight { get; set; }
        public bool ApplyMiscellaneous { get; set; }
        public virtual SOP00021 SOP00021 { get; set; }
        public virtual ICollection<SOP00200> SOP00200 { get; set; }
    }
}
