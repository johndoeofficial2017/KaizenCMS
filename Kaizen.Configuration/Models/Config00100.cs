using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Config00100
    {
        public string CompanyID { get; set; }
        public bool IsAutoItemID { get; set; }
        public bool IsAutoItemIDByCat { get; set; }
        public virtual Company Company { get; set; }
    }
}
