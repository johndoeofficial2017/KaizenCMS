using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00105
    {
        public int ContactTypeID { get; set; }
        public string CUSTNMBR { get; set; }
        public string ContactPerson { get; set; }
        public string PersonPosition { get; set; }
        public string PersonEmailAdd { get; set; }
        public string OtherInfo { get; set; }
        public virtual SOP00100 SOP00100 { get; set; }
        public virtual Sys00006 Sys00006 { get; set; }
    }
}
