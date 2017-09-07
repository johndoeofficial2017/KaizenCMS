using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00014
    {
        public Sys00014()
        {
            this.Sys00001 = new List<Sys00001>();
        }

        public string CityID { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<Sys00001> Sys00001 { get; set; }
    }
}
