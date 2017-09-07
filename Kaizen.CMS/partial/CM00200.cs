using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00200
    {
        [NotMapped]
        public CM00111 BillAddress { get; set; }
    }
}
