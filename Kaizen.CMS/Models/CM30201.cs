using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM30201
    {
        public string CIFNumber { get; set; }
        public string DebtorID { get; set; }
        public string EnglishFullName { get; set; }
        public virtual CM30200 CM30200 { get; set; }
    }
}
