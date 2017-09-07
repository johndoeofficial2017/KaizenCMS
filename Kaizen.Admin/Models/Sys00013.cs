using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00013
    {
        public Sys00013()
        {
            this.Sys00001 = new List<Sys00001>();
        }

        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public virtual ICollection<Sys00001> Sys00001 { get; set; }
    }
}
