using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP10202
    {
        public SOP10202()
        {
            this.SOP10201 = new List<SOP10201>();
        }

        public int ItemCategoryID { get; set; }
        public string ItemCategoryName { get; set; }
        public string SOPNUMBE { get; set; }
        public string SOPTypeSetupID { get; set; }
        public byte SOPTYPE { get; set; }
        public virtual ICollection<SOP10201> SOP10201 { get; set; }
    }
}
