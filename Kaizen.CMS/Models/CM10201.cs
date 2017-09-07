using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10201
    {
        public string CIFNumber { get; set; }
        public string DebtorID { get; set; }
        public virtual CM00100 CM00100 { get; set; }
    }
}
