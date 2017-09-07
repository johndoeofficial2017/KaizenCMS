using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00210
    {
        [NotMapped]
        public CM00104 Address { get; set; }
    }
}
