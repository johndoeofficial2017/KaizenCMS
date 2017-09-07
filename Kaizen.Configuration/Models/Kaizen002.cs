using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen002
    {
        public Kaizen002()
        {
            this.Kaizen004 = new List<Kaizen004>();
        }

        public int TaskID { get; set; }
        public int ModuleID { get; set; }
        public int MenueTypeID { get; set; }
        public string MenuName { get; set; }
        public string ScreenPath { get; set; }
        public Nullable<bool> IsCustomPage { get; set; }
        public Nullable<int> MenueOrder { get; set; }
        public string WindowPath { get; set; }
        public string WindowParm { get; set; }
        public virtual Kaizen000 Kaizen000 { get; set; }
        public virtual Kaizen001 Kaizen001 { get; set; }
        public virtual ICollection<Kaizen004> Kaizen004 { get; set; }
    }
}
