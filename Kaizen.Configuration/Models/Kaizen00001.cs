using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00001
    {
        public Kaizen00001()
        {
            this.Kaizen00002 = new List<Kaizen00002>();
        }

        public int FieldID { get; set; }
        public int ScreenID { get; set; }
        public string FieldName { get; set; }
        public string kaizenTableName { get; set; }
        public bool IsDynamicTable { get; set; }
        public virtual ICollection<Kaizen00002> Kaizen00002 { get; set; }
        public virtual Kaizen00010 Kaizen00010 { get; set; }
    }
}
