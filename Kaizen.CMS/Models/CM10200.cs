using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM10200
    {
        public string CIFNumber { get; set; }
        public string CIFName { get; set; }
        public string AddressName { get; set; }
        public Nullable<bool> IsJoint { get; set; }
        public string DescriptionNote { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
