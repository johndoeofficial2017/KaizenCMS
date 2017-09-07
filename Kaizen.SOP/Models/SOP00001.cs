using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00001
    {
        public string SOPTypeSetupID { get; set; }
        public string SOPTypeSetupName { get; set; }
        public byte SOPTYPE { get; set; }
        public int NextDocumentNumber { get; set; }
        public string NextNumberPrefix { get; set; }
        public byte NextNumberlenth { get; set; }
        public Nullable<bool> UseProspect { get; set; }
        public Nullable<bool> IsRepeatable { get; set; }
        public Nullable<bool> IsOverride { get; set; }
        public string SOPTypeSetupBackOrder { get; set; }
        public string SOPTypeSetupFulfillment { get; set; }
        public string SOPTypeSetupInvoice { get; set; }
        public virtual SOP00000 SOP00000 { get; set; }
    }
}
