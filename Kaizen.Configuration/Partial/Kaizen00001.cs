using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Configuration
{
    public partial class Kaizen00001
    {
        [NotMapped]
        public string FieldValue { get; set; }
        [NotMapped]
        public string SourceValue { get; set; }
        [NotMapped]
        public string FixedValue { get; set; }
        [NotMapped]
        public ExcelColumns[] Details { get; set; }
    }
}

