using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class IV00013
    {
        public IV00013()
        {
            this.IV00014 = new List<IV00014>();
        }

        public string CollCode { get; set; }
        public string CollName { get; set; }
        public string LotNumber { get; set; }
        public int CollType { get; set; }
        public string LookupFrom { get; set; }
        public string LookupLotNumber { get; set; }
        public string LookupLotName { get; set; }
        public virtual ICollection<IV00014> IV00014 { get; set; }
    }
}
