using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00101
    {
        public Sys00101()
        {
            this.Sys00102 = new List<Sys00102>();
        }

        public int ActionID { get; set; }
        public int TaskID { get; set; }
        public double TaskProgress { get; set; }
        public string TaskDescription { get; set; }
        public System.DateTime TaskDate { get; set; }
        public string UserName { get; set; }
        public string PhotoExtension { get; set; }
        public string UserAsginTO { get; set; }
        public virtual Sys00100 Sys00100 { get; set; }
        public virtual ICollection<Sys00102> Sys00102 { get; set; }
    }
}
