using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class Sys00030
    {
        public Sys00030()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public string NationalityID { get; set; }
        public string NationalityName { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
