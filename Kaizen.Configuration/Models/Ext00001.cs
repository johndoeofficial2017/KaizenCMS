using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Ext00001
    {
        public Ext00001()
        {
            this.Ext00002 = new List<Ext00002>();
        }

        public int DataBaseSourceID { get; set; }
        public string CompanyUserName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPassword { get; set; }
        public virtual ICollection<Ext00002> Ext00002 { get; set; }
    }
}
