using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10108
    {
        public int LineID { get; set; }
        public string SOPNUMBE { get; set; }
        public int AccountID { get; set; }
        public string SourceID { get; set; }
        public decimal DEBITAMT { get; set; }
        public decimal CRDTAMNT { get; set; }
        public decimal ORDBTAMT { get; set; }
        public decimal ORCRDAMT { get; set; }
        public string LineReference { get; set; }
        public string LineDescription { get; set; }
        public int KaizenID { get; set; }
        public virtual GL00100 GL00100 { get; set; }
        public virtual SOP10100 SOP10100 { get; set; }
    }
}
