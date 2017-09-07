using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.SOP
{
    public partial class SOP10203
    {
        [NotMapped]
        public int status { get; set; }

    }
}
