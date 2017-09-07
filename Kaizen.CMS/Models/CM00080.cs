using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00080
    {
        public string FieldName { get; set; }
        public int TRXTypeID { get; set; }
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
    }
}
