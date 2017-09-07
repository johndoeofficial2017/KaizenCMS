using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00029
    {
        public CM00029()
        {
            this.CM00057 = new List<CM00057>();
            this.CM00039 = new List<CM00039>();
            this.CM00056 = new List<CM00056>();
            this.CM00070 = new List<CM00070>();
            this.CM00071 = new List<CM00071>();
            this.CM00074 = new List<CM00074>();
            this.CM00203 = new List<CM00203>();
        }

        public int TRXTypeID { get; set; }
        public string TrxTypeName { get; set; }
        public bool IsCaseFixedSerial { get; set; }
        public string NextDocumentNumber { get; set; }
        public string NextNumberPrefix { get; set; }
        public Nullable<byte> NextNumberlenth { get; set; }
        public string StatusHTML { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public string TableSource { get; set; }
        public virtual ICollection<CM00057> CM00057 { get; set; }
        public virtual CM00037 CM00037 { get; set; }
        public virtual ICollection<CM00039> CM00039 { get; set; }
        public virtual CM00048 CM00048 { get; set; }
        public virtual CM00049 CM00049 { get; set; }
        public virtual ICollection<CM00056> CM00056 { get; set; }
        public virtual ICollection<CM00070> CM00070 { get; set; }
        public virtual ICollection<CM00071> CM00071 { get; set; }
        public virtual ICollection<CM00074> CM00074 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
