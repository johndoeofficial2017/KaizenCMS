using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00089
    {
        public CM00089()
        {
            this.CM00090 = new List<CM00090>();
            this.CM00091 = new List<CM00091>();
        }

        public string CheckbookCode { get; set; }
        public string CurrencyCode { get; set; }
        public string CheckbookName { get; set; }
        public int TrxPaymentType { get; set; }
        public bool ToAgent { get; set; }
        public string ToAgentID { get; set; }
        public virtual CM00088 CM00088 { get; set; }
        public virtual ICollection<CM00090> CM00090 { get; set; }
        public virtual ICollection<CM00091> CM00091 { get; set; }
    }
}
