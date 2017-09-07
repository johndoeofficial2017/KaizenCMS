using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00104
    {
        [NotMapped]
        public bool IsSeleceted { get; set; }
    }
}
