using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class UPR00030
    {
        public UPR00030()
        {
            this.SOP00100 = new List<SOP00100>();
        }

        public string NationalityID { get; set; }
        public string NationalityName { get; set; }
        public virtual ICollection<SOP00100> SOP00100 { get; set; }
    }
}
