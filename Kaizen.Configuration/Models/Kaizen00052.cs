using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00052
    {
        public Kaizen00052()
        {
            this.Kaizen00053 = new List<Kaizen00053>();
        }

        public int ReportID { get; set; }
        public int ReportTypeID { get; set; }
        public string ReportName { get; set; }
        public virtual Kaizen00051 Kaizen00051 { get; set; }
        public virtual ICollection<Kaizen00053> Kaizen00053 { get; set; }
    }
}
