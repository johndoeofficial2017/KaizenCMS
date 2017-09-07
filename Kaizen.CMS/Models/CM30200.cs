using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM30200
    {
        public CM30200()
        {
            this.CM30201 = new List<CM30201>();
        }

        public string CIFNumber { get; set; }
        public string CIFName { get; set; }
        public string AddressName { get; set; }
        public string DescriptionNote { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<CM30201> CM30201 { get; set; }
    }
}
