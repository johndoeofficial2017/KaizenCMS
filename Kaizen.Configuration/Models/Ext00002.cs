using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Ext00002
    {
        public Ext00002()
        {
            this.Ext00003 = new List<Ext00003>();
            this.Ext00004 = new List<Ext00004>();
            this.Ext00005 = new List<Ext00005>();
        }

        public int DataBaseSourceID { get; set; }
        public string ComTableName { get; set; }
        public string SourceDisplay { get; set; }
        public Nullable<bool> IsAddNew { get; set; }
        public Nullable<bool> IsformOnly { get; set; }
        public Nullable<bool> IsView { get; set; }
        public Nullable<bool> IsGraph { get; set; }
        public int ModuleID { get; set; }
        public int MenueTypeID { get; set; }
        public string CompanyID { get; set; }
        public virtual Ext00001 Ext00001 { get; set; }
        public virtual ICollection<Ext00003> Ext00003 { get; set; }
        public virtual ICollection<Ext00004> Ext00004 { get; set; }
        public virtual ICollection<Ext00005> Ext00005 { get; set; }
    }
}
