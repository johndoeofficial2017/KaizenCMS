using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00004
    {
        public byte ModuleID { get; set; }
        public string CompanyID { get; set; }
        public string UserCode { get; set; }
        public virtual Sys00003 Sys00003 { get; set; }
    }
}
