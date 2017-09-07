using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10506
    {
        public int ItemLineIndex { get; set; }
        public string SOPNUMBE { get; set; }
        public string ShipAddressTypeCode { get; set; }
        public string ShipAddressName { get; set; }
        public string Pnone01 { get; set; }
        public string Pnone02 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string POBox { get; set; }
        public string Other01 { get; set; }
        public string Other02 { get; set; }
        public string Address1 { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
        public virtual SOP10501 SOP10501 { get; set; }
    }
}
