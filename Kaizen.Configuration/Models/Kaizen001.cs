using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen001
    {
        public Kaizen001()
        {
            this.DT00100 = new List<DT00100>();
            this.Kaizen002 = new List<Kaizen002>();
        }

        public int MenueTypeID { get; set; }
        public string MenueTypeName { get; set; }
        public virtual ICollection<DT00100> DT00100 { get; set; }
        public virtual ICollection<Kaizen002> Kaizen002 { get; set; }
    }
}
