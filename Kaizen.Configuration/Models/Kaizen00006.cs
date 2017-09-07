using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00006
    {
        public Kaizen00006()
        {
            this.DT00101 = new List<DT00101>();
            this.Ext00003 = new List<Ext00003>();
            this.Kaizen00003 = new List<Kaizen00003>();
            this.Kaizen00008 = new List<Kaizen00008>();
            this.Kaizen00013 = new List<Kaizen00013>();
        }

        public short FieldTypeID { get; set; }
        public string FieldTypeName { get; set; }
        public virtual ICollection<DT00101> DT00101 { get; set; }
        public virtual ICollection<Ext00003> Ext00003 { get; set; }
        public virtual ICollection<Kaizen00003> Kaizen00003 { get; set; }
        public virtual ICollection<Kaizen00008> Kaizen00008 { get; set; }
        public virtual ICollection<Kaizen00013> Kaizen00013 { get; set; }
    }
}
