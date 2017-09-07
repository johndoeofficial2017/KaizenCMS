using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Ext00005
    {
        public int DataBaseSourceID { get; set; }
        public string ComTableName { get; set; }
        public string UserName { get; set; }
        public bool IsDefault { get; set; }
        public virtual Ext00002 Ext00002 { get; set; }
    }
}
