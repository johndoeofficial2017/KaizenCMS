using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00020
    {
        public SOP00020()
        {
            this.SOP00200 = new List<SOP00200>();
        }

        public byte TRXTypeID { get; set; }
        public string TrxTypeName { get; set; }
        public string NextDocumentNumber { get; set; }
        public string NextNumberPrefix { get; set; }
        public Nullable<byte> NextNumberlenth { get; set; }
        public virtual ICollection<SOP00200> SOP00200 { get; set; }
    }
}
