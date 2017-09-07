using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00050
    {
        public Kaizen00050()
        {
            this.Kaizen00053 = new List<Kaizen00053>();
            this.Kaizen00054 = new List<Kaizen00054>();
            this.Kaizen00055 = new List<Kaizen00055>();
        }

        public int DashboardCode { get; set; }
        public string DashboardName { get; set; }
        public string CompanyID { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Kaizen00053> Kaizen00053 { get; set; }
        public virtual ICollection<Kaizen00054> Kaizen00054 { get; set; }
        public virtual ICollection<Kaizen00055> Kaizen00055 { get; set; }
    }
}
