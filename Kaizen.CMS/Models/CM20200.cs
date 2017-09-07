using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM20200
    {
        public CM20200()
        {
            this.CM20201 = new List<CM20201>();
        }

        public string CIFNumber { get; set; }
        public string CIFName { get; set; }
        public string AddressName { get; set; }
        public string DescriptionNote { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<CM20201> CM20201 { get; set; }
    }
}
