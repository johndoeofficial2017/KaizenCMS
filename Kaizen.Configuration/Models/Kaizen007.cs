using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen007
    {
        public int RoleID { get; set; }
        public string CompanyID { get; set; }
        public string TRXTypeID { get; set; }
        public virtual Kaizen006 Kaizen006 { get; set; }
    }
}
