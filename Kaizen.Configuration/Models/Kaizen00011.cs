using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00011
    {
        public Kaizen00011()
        {
            this.Kaizen00026 = new List<Kaizen00026>();
            this.Kaizen00014 = new List<Kaizen00014>();
            this.KaizenGridViewAccesses = new List<KaizenGridViewAccess>();
            this.Kaizen00030 = new List<Kaizen00030>();
        }

        public int ViewID { get; set; }
        public int ScreenID { get; set; }
        public string CompanyID { get; set; }
        public string ViewName { get; set; }
        public string ViewDescription { get; set; }
        public bool IsEmailable { get; set; }
        public bool IsPrintable { get; set; }
        public bool IsPivotTable { get; set; }
        public bool IsDefault { get; set; }
        public bool IsCustomView { get; set; }
        public string ControllerSource { get; set; }
        public string ViewWhereCondition { get; set; }
        public string ViewSortCondition { get; set; }
        public string ViewStateData { get; set; }
        public virtual Company Company { get; set; }
        public virtual Kaizen00010 Kaizen00010 { get; set; }
        public virtual ICollection<Kaizen00026> Kaizen00026 { get; set; }
        public virtual ICollection<Kaizen00014> Kaizen00014 { get; set; }
        public virtual ICollection<KaizenGridViewAccess> KaizenGridViewAccesses { get; set; }
        public virtual ICollection<Kaizen00030> Kaizen00030 { get; set; }
    }
}
