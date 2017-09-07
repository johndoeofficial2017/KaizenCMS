using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Ext00003
    {
        public int DataBaseSourceID { get; set; }
        public string ComTableName { get; set; }
        public string FieldName { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldDisplay { get; set; }
        public bool IsColumn01 { get; set; }
        public bool IsRequired { get; set; }
        public int FieldOrder { get; set; }
        public int FieldWidth { get; set; }
        public virtual Ext00002 Ext00002 { get; set; }
        public virtual Kaizen00006 Kaizen00006 { get; set; }
    }
}
