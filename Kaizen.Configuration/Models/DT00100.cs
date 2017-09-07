using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class DT00100
    {
        public DT00100()
        {
            this.DT00101 = new List<DT00101>();
            this.DT00102 = new List<DT00102>();
            this.DT00103 = new List<DT00103>();
        }

        public int SourceID { get; set; }
        public string SourceName { get; set; }
        public string SourceDisplay { get; set; }
        public string SourceSql { get; set; }
        public int ModuleID { get; set; }
        public int MenueTypeID { get; set; }
        public string CompanyID { get; set; }
        public Nullable<bool> IsGraph { get; set; }
        public bool IsDataTable { get; set; }
        public Nullable<bool> IsAddNew { get; set; }
        public Nullable<bool> IsEdit { get; set; }
        public string SourceHTML { get; set; }
        public Nullable<int> ViewType { get; set; }
        public Nullable<int> ViewSource { get; set; }
        public virtual Company Company { get; set; }
        public virtual DT00001 DT00001 { get; set; }
        public virtual Kaizen000 Kaizen000 { get; set; }
        public virtual Kaizen001 Kaizen001 { get; set; }
        public virtual ICollection<DT00101> DT00101 { get; set; }
        public virtual ICollection<DT00102> DT00102 { get; set; }
        public virtual ICollection<DT00103> DT00103 { get; set; }
    }
}
