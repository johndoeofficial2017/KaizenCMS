using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00507
    {
        public int AccountLine { get; set; }
        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public byte SOPTYPE { get; set; }
        public string TrxDocumentID { get; set; }
        public int AccountID { get; set; }
        public string ACTNUMBR { get; set; }
        public string AccountName { get; set; }
        public string SourceID { get; set; }
        public double DebitAMT { get; set; }
        public double CreditAMT { get; set; }
        public double DebitApplyAMT { get; set; }
        public double CrebitApplyAMT { get; set; }
        public string GLReference { get; set; }
        public virtual SOP00503 SOP00503 { get; set; }
    }
}
