using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10306
    {
        public int LineID { get; set; }
        public string SOPNUMBE { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public double QTY { get; set; }
        public string Description { get; set; }
    }
}
