using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00090
    {
        public string UserName { get; set; }
        public string CheckbookCode { get; set; }
        public string CurrencyCode { get; set; }
        public virtual CM00089 CM00089 { get; set; }
    }
}
