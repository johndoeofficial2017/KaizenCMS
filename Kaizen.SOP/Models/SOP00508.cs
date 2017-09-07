using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00508
    {
        public int AccountLine { get; set; }
        public string TrxDocumentID { get; set; }
        public int AccountID { get; set; }
        public string ACTNUMBR { get; set; }
        public string AccountName { get; set; }
        public string SourceID { get; set; }
        public double DebitAMT { get; set; }
        public double CreditAMT { get; set; }
        public string GLReference { get; set; }
        public virtual SOP00500 SOP00500 { get; set; }
    }
}
