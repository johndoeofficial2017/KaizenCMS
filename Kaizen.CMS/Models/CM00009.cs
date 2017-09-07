using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00009
    {
        public CM00009()
        {
            this.CM00031 = new List<CM00031>();
            this.CM00032 = new List<CM00032>();
            this.CM00104 = new List<CM00104>();
        }

        public string AddressCode { get; set; }
        public string AddressName { get; set; }
        public virtual ICollection<CM00031> CM00031 { get; set; }
        public virtual ICollection<CM00032> CM00032 { get; set; }
        public virtual ICollection<CM00104> CM00104 { get; set; }
    }
}
