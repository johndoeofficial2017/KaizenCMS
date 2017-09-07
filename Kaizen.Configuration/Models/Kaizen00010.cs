using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00010
    {
        public Kaizen00010()
        {
            this.Kaizen00001 = new List<Kaizen00001>();
            this.Kaizen00007 = new List<Kaizen00007>();
            this.Kaizen00025 = new List<Kaizen00025>();
            this.Kaizen00011 = new List<Kaizen00011>();
            this.Kaizen00013 = new List<Kaizen00013>();
        }

        public int ScreenID { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public string ScreenName { get; set; }
        public string ScreenDescription { get; set; }
        public bool HasSubScreen { get; set; }
        public string MainTable { get; set; }
        public virtual Kaizen000 Kaizen000 { get; set; }
        public virtual ICollection<Kaizen00001> Kaizen00001 { get; set; }
        public virtual ICollection<Kaizen00007> Kaizen00007 { get; set; }
        public virtual ICollection<Kaizen00025> Kaizen00025 { get; set; }
        public virtual ICollection<Kaizen00011> Kaizen00011 { get; set; }
        public virtual ICollection<Kaizen00013> Kaizen00013 { get; set; }
    }
}
