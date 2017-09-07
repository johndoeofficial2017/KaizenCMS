using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.SOP
{
    public partial class SOP10501
    {
        [NotMapped]
        public ICollection<SOP10505> SOP10505 { get; set; }
    }
}
