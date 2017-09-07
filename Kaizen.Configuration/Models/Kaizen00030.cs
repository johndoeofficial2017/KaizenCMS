using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00030
    {
        public Kaizen00030()
        {
            this.Kaizen004 = new List<Kaizen004>();
            this.Kaizen006 = new List<Kaizen006>();
            this.KaizenUserRoles = new List<KaizenUserRole>();
            this.Kaizen00011 = new List<Kaizen00011>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Kaizen004> Kaizen004 { get; set; }
        public virtual ICollection<Kaizen006> Kaizen006 { get; set; }
        public virtual ICollection<KaizenUserRole> KaizenUserRoles { get; set; }
        public virtual ICollection<Kaizen00011> Kaizen00011 { get; set; }
    }
}
