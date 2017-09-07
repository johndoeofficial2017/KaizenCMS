using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.SOP
{
    public partial class SOP10201
    {
        [NotMapped]
        public ICollection<IV00013> IV00013 { get; set; }
        [NotMapped]
        public string LotNumber { get; set; }

    }
}
