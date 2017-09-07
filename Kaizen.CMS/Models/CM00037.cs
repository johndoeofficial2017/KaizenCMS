using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00037
    {
        public CM00037()
        {
            this.CM00029 = new List<CM00029>();
        }

        public string TableSource { get; set; }
        public string SourceTypeName { get; set; }
        public bool IsCustomTable { get; set; }
        public virtual ICollection<CM00029> CM00029 { get; set; }
    }
}
