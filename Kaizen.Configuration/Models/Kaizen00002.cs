using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00002
    {
        public Kaizen00002()
        {
            this.Kaizen00003 = new List<Kaizen00003>();
        }

        public int ScreenID { get; set; }
        public int FieldID { get; set; }
        public string FieldValue { get; set; }
        public virtual Kaizen00001 Kaizen00001 { get; set; }
        public virtual ICollection<Kaizen00003> Kaizen00003 { get; set; }
    }
}
