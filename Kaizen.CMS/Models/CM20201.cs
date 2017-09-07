using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM20201
    {
        public string CIFNumber { get; set; }
        public string DebtorID { get; set; }
        public string EnglishFullName { get; set; }
        public virtual CM20200 CM20200 { get; set; }
    }
}
