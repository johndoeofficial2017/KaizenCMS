using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00032
    {
        public string AddressCode { get; set; }
        public int RoleID { get; set; }
        public virtual CM00009 CM00009 { get; set; }
    }
}
