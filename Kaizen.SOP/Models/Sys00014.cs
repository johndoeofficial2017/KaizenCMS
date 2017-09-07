using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class Sys00014
    {
        public Sys00014()
        {
            this.SOP00101 = new List<SOP00101>();
        }

        public string CityID { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<SOP00101> SOP00101 { get; set; }
    }
}
