using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Prn00100
    {
        public int TrxID { get; set; }
        public string CUSTNMBR { get; set; }
        public string UserName { get; set; }
        public string PCName { get; set; }
        public string DisplayName { get; set; }
        public string PCIP { get; set; }
        public string PrinterName { get; set; }
        public string PrintedFile { get; set; }
        public string EmailToList { get; set; }
        public string EmailCCList { get; set; }
        public string EmailBCCList { get; set; }
    }
}
