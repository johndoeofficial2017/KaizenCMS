using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen004View
    {
        public int TaskID { get; set; }
        public string UserName { get; set; }
        public int ModuleID { get; set; }
        public string MenuName { get; set; }
        public string ScreenPath { get; set; }
        public int MenueTypeID { get; set; }
        public int MenueOrder { get; set; }
        public bool IsCustomPage { get; set; }
        public string CompanyID { get; set; }
        public string WindowPath { get; set; }
    }
}
