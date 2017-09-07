using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00100
    {
        public Sys00100()
        {
            this.Sys00101 = new List<Sys00101>();
            this.Sys00103 = new List<Sys00103>();
        }

        public int TaskID { get; set; }
        public string TaskTypeID { get; set; }
        public string PriorityID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public System.DateTime TaskStartDate { get; set; }
        public System.DateTime TaskEndDate { get; set; }
        public string UserAsginFrom { get; set; }
        public string UserAsginTO { get; set; }
        public System.DateTime AssignDate { get; set; }
        public double TaskProgress { get; set; }
        public virtual ICollection<Sys00101> Sys00101 { get; set; }
        public virtual ICollection<Sys00103> Sys00103 { get; set; }
    }
}
