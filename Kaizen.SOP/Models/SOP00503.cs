using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00503
    {
        public SOP00503()
        {
            this.SOP00507 = new List<SOP00507>();
        }

        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public byte SOPTYPE { get; set; }
        public string TrxDocumentID { get; set; }
        public System.DateTime DOCDATE { get; set; }
        public string CustomerPoNumber { get; set; }
        public double DOCAMNT { get; set; }
        public double ORGAplyAMT { get; set; }
        public string CurrencyCode { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsMultiply { get; set; }
        public byte DecimalDigit { get; set; }
        public double WriteOff { get; set; }
        public virtual SOP00500 SOP00500 { get; set; }
        public virtual ICollection<SOP00507> SOP00507 { get; set; }
    }
}
