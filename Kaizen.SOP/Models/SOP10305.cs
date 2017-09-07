using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10305
    {
        public string VoucherTrxNumber { get; set; }
        public double VoucherAMT { get; set; }
        public int VoucherCount { get; set; }
        public System.DateTime VoucherStartDate { get; set; }
        public System.DateTime VoucherEndDate { get; set; }
        public string UserName { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string BarcodPrefix { get; set; }
        public byte BarcodLenth { get; set; }
        public byte BarcodStartNumber { get; set; }
        public string TrxComments { get; set; }
    }
}
