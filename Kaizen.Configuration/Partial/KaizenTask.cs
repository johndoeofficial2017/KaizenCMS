using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Configuration
{
    public partial class KaizenTask
    {
        [NotMapped]
        public bool IsCommon { get; set; }
        [NotMapped]
        public string ScreenPath { get; set; }
    }
}
