using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00016
    {
        public Kaizen00016()
        {
            this.Kaizen00015 = new List<Kaizen00015>();
        }

        public string TRXTypeID { get; set; }
        public string CompanyID { get; set; }
        public string TrxTypeName { get; set; }
        public virtual ICollection<Kaizen00015> Kaizen00015 { get; set; }
    }
}
