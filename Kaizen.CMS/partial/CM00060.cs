using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00060
    {
        [NotMapped]
        public string FieldValue { get; set; }
        [NotMapped]
        public System.DateTime FieldDateValue { get; set; }
    }
}
