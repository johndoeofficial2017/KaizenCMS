using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen004
    {
        public string CompanyID { get; set; }
        public int RoleID { get; set; }
        public int TaskID { get; set; }
        public int ModuleID { get; set; }
        public int MenueTypeID { get; set; }
        public string MenuName { get; set; }
        public string ScreenPath { get; set; }
        public bool IsCustomPage { get; set; }
        public int MenueOrder { get; set; }
        public string WindowPath { get; set; }
        public virtual Kaizen00030 Kaizen00030 { get; set; }
        public virtual Kaizen00101 Kaizen00101 { get; set; }
        public virtual Kaizen002 Kaizen002 { get; set; }
    }
}
