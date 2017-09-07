using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class PROJ00100
    {
        public PROJ00100()
        {
            this.SOP10201 = new List<SOP10201>();
        }

        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public virtual ICollection<SOP10201> SOP10201 { get; set; }
    }
}
