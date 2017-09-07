using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM10110
    {
        [NotMapped]
        public CM00007 CM00007 { get; set; }
    }
}
