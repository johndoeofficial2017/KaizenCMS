using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00003
    {
        public Kaizen00003()
        {
            this.Kaizen00009 = new List<Kaizen00009>();
        }

        public string ExtraFieldID { get; set; }
        public int ScreenID { get; set; }
        public int FieldID { get; set; }
        public string FieldValue { get; set; }
        public short FieldTypeID { get; set; }
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string kaizenTableName { get; set; }
        public virtual Kaizen00002 Kaizen00002 { get; set; }
        public virtual Kaizen00006 Kaizen00006 { get; set; }
        public virtual ICollection<Kaizen00009> Kaizen00009 { get; set; }
    }
}
