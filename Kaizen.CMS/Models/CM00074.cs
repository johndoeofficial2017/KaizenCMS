using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00074
    {
        public CM00074()
        {
            this.CM00075 = new List<CM00075>();
        }

        public int TRXTypeID { get; set; }
        public string FieldName { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldDisplay { get; set; }
        public bool IsRequired { get; set; }
        public int FieldOrder { get; set; }
        public int FieldWidth { get; set; }
        public Nullable<bool> IsEmailable { get; set; }
        public string FieldEquation { get; set; }
        public bool IsPrintable { get; set; }
        public bool IsActive { get; set; }
        public bool Islocked { get; set; }
        public Nullable<bool> Islockable { get; set; }
        public Nullable<bool> IsFilterable { get; set; }
        public bool IsSortable { get; set; }
        public virtual CM00029 CM00029 { get; set; }
        public virtual ICollection<CM00075> CM00075 { get; set; }
    }
}
