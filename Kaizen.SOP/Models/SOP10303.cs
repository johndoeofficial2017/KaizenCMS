using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10303
    {
        public int LineID { get; set; }
        public string SOPNUMBE { get; set; }
        public double DOCAMNT { get; set; }
        public string CheckNumber { get; set; }
        public string CheckDate { get; set; }
        public string CheckBankName { get; set; }
        public virtual SOP10300 SOP10300 { get; set; }
    }
}
