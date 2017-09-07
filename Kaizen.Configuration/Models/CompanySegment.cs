using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class CompanySegment
    {
        public int SegmentID { get; set; }
        public string SegmentName { get; set; }
        public string CompanyID { get; set; }
        public int SegmentLength { get; set; }
        public virtual Company Company { get; set; }
    }
}
