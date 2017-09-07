using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen000
    {
        public Kaizen000()
        {
            this.DT00100 = new List<DT00100>();
            this.Kaizen00010 = new List<Kaizen00010>();
            this.Kaizen003 = new List<Kaizen003>();
            this.Kaizen002 = new List<Kaizen002>();
        }

        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public virtual ICollection<DT00100> DT00100 { get; set; }
        public virtual ICollection<Kaizen00010> Kaizen00010 { get; set; }
        public virtual ICollection<Kaizen003> Kaizen003 { get; set; }
        public virtual ICollection<Kaizen002> Kaizen002 { get; set; }
    }
}
