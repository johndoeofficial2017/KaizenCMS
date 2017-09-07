using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00106
    {
        public int ContactTypeID { get; set; }
        public string DebtorID { get; set; }
        public string ContactPerson { get; set; }
        public string PersonPosition { get; set; }
        public string PersonEmailAdd { get; set; }
        public string Pnone01 { get; set; }
        public string Pnone02 { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string OtherInfo { get; set; }
        public virtual CM00100 CM00100 { get; set; }
    }
}
