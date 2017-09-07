using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00051
    {
        public Kaizen00051()
        {
            this.Kaizen00052 = new List<Kaizen00052>();
        }

        public int ReportTypeID { get; set; }
        public string ReportTypeName { get; set; }
        public virtual ICollection<Kaizen00052> Kaizen00052 { get; set; }
    }
}
