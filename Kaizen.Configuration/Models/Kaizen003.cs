using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen003
    {
        public int TaskID { get; set; }
        public int ModuleID { get; set; }
        public string MenuName { get; set; }
        public string ScreenPath { get; set; }
        public virtual Kaizen000 Kaizen000 { get; set; }
    }
}
