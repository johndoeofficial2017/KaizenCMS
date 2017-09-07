using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00007
    {
        public Kaizen00007()
        {
            this.Kaizen00008 = new List<Kaizen00008>();
        }

        public int EXT_ScreenID { get; set; }
        public int ScreenID { get; set; }
        public string ScreenName { get; set; }
        public string ScreenDescription { get; set; }
        public virtual Kaizen00010 Kaizen00010 { get; set; }
        public virtual ICollection<Kaizen00008> Kaizen00008 { get; set; }
    }
}
