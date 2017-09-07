using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00025
    {
        [NotMapped]
        public System.DateTime TaskStartDate { get; set; }
        [NotMapped]
        public System.DateTime TaskEndDate { get; set; }
        [NotMapped]
        public System.DateTime AssignDate { get; set; }
    }
}
