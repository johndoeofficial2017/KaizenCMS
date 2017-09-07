using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Configuration
{
    public partial class Kaizen00015
    {
        [NotMapped]
        public string SourceValue { get; set; }
        [NotMapped]
        public string FixedValue { get; set; }
    }
}

