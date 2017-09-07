using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00100
    {
        [NotMapped]
        public CM00104 MainAddress { get; set; }
        [NotMapped]
        public CM00104 BillAddress { get; set; }
        [NotMapped]
        public CM00104 StatementAddress { get; set; }
    }
}
