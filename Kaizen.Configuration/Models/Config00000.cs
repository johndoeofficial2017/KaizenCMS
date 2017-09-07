using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Config00000
    {
        public int TableID { get; set; }
        public Nullable<bool> IsExpiryDate { get; set; }
        public Nullable<int> ExpiryMonthDate { get; set; }
        public Nullable<int> ExpirtyPeriodDays { get; set; }
    }
}
