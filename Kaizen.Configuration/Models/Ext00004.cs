using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Ext00004
    {
        public int RoleID { get; set; }
        public int DataBaseSourceID { get; set; }
        public string ComTableName { get; set; }
        public virtual Ext00002 Ext00002 { get; set; }
    }
}
