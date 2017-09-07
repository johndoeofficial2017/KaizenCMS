using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00071
    {
        public CM00071()
        {
            this.CM00072 = new List<CM00072>();
            this.CM00073 = new List<CM00073>();
            this.CM00075 = new List<CM00075>();
            this.CM00076 = new List<CM00076>();
            this.CM00081 = new List<CM00081>();
            this.CM00082 = new List<CM00082>();
            this.CM00083 = new List<CM00083>();
            this.CM00084 = new List<CM00084>();
            this.CM00085 = new List<CM00085>();
            this.CM00086 = new List<CM00086>();
        }

        public int ViewID { get; set; }
        public int TRXTypeID { get; set; }
        public string ViewName { get; set; }
        public string ViewDescription { get; set; }
        public bool IsEmailable { get; set; }
        public bool IsPrintable { get; set; }
        public bool IsPivotTable { get; set; }
        public bool IsGraph { get; set; }
        public bool IsSummery { get; set; }
        public bool IsDefault { get; set; }
        public bool IsReminder { get; set; }
        public string WhereCondition { get; set; }
        public string WhereCustomCondition { get; set; }
        public string ViewSortCondition { get; set; }
        public string TableSource { get; set; }
        public virtual CM00029 CM00029 { get; set; }
        public virtual ICollection<CM00072> CM00072 { get; set; }
        public virtual ICollection<CM00073> CM00073 { get; set; }
        public virtual ICollection<CM00075> CM00075 { get; set; }
        public virtual ICollection<CM00076> CM00076 { get; set; }
        public virtual ICollection<CM00081> CM00081 { get; set; }
        public virtual ICollection<CM00082> CM00082 { get; set; }
        public virtual ICollection<CM00083> CM00083 { get; set; }
        public virtual ICollection<CM00084> CM00084 { get; set; }
        public virtual ICollection<CM00085> CM00085 { get; set; }
        public virtual ICollection<CM00086> CM00086 { get; set; }
    }
}
