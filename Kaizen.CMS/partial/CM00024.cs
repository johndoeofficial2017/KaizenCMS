using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00700
    {
        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
