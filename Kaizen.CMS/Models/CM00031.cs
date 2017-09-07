using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00031
    {
        public string UserName { get; set; }
        public string AddressCode { get; set; }
        public virtual CM00009 CM00009 { get; set; }
    }
}
