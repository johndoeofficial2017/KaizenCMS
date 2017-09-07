using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00024
    {
        public Kaizen00024()
        {
            this.Kaizen00025 = new List<Kaizen00025>();
        }

        public string FieldTypeID { get; set; }
        public string FieldTypeName { get; set; }
        public virtual ICollection<Kaizen00025> Kaizen00025 { get; set; }
    }
}
