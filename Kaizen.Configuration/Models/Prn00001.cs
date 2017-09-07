using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Prn00001
    {
        public Prn00001()
        {
            this.Prn00101 = new List<Prn00101>();
        }

        public int PrnCatTypeID { get; set; }
        public string PrnCatTypeName { get; set; }
        public virtual ICollection<Prn00101> Prn00101 { get; set; }
    }
}
