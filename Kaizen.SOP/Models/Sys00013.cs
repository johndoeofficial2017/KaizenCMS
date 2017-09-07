using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class Sys00013
    {
        public Sys00013()
        {
            this.SOP00101 = new List<SOP00101>();
        }

        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public virtual ICollection<SOP00101> SOP00101 { get; set; }
    }
}
