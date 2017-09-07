using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Config00001
    {
        public string CompanyID { get; set; }
        public bool IsAutoItemID { get; set; }
        public bool IsAutoItemIDByCat { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<int> LastEmployeeID { get; set; }
    }
}
