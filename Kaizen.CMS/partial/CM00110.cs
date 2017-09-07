using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00110
    {
        [NotMapped]
        public CM00111 MainAddress { get; set; }
        [NotMapped]
        public CM00111 BillAddress { get; set; }
        [NotMapped]
        public CM00111 RemitAddress { get; set; }
        [NotMapped]
        public DateTime Created { get; set; }
        [NotMapped]
        public bool Checked { get; set; }
    }
}
