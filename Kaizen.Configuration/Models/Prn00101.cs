using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Prn00101
    {
        public int PrnID { get; set; }
        public string UserName { get; set; }
        public string PCName { get; set; }
        public string DisplayName { get; set; }
        public Nullable<int> PrnCatTypeID { get; set; }
        public string PrnCatTypeName { get; set; }
        public string PCIP { get; set; }
        public string PrinterName { get; set; }
        public string PrintedFile { get; set; }
        public string EmailToList { get; set; }
        public string EmailCCList { get; set; }
        public string EmailBCCList { get; set; }
        public virtual Prn00001 Prn00001 { get; set; }
    }
}
