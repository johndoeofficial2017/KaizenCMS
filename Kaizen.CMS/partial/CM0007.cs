using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00007
    {
        [NotMapped]
        public double TotalTarget { get; set; }
        [NotMapped]
        public double BalanceTarget { get; set; }
    }
}
