using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00501
    {
        public SOP00501()
        {
            this.SOP00500 = new List<SOP00500>();
        }

        public string BatchID { get; set; }
        public Nullable<double> BatchAmount { get; set; }
        public string BatchDescription { get; set; }
        public Nullable<int> BatchTRXcount { get; set; }
        public Nullable<bool> IsTransactionDate { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public virtual ICollection<SOP00500> SOP00500 { get; set; }
    }
}
