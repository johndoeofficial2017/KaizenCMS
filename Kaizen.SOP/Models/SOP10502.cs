using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10502
    {
        public SOP10502()
        {
            this.SOP10503 = new List<SOP10503>();
        }

        public int LotRowID { get; set; }
        public int ItemLineIndex { get; set; }
        public string SOPNUMBE { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> LOTLineCode { get; set; }
        public double AppliedQuantity { get; set; }
        public virtual SOP10501 SOP10501 { get; set; }
        public virtual ICollection<SOP10503> SOP10503 { get; set; }
    }
}
