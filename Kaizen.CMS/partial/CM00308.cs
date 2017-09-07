using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00308
    {
        [NotMapped]
        public double ClaimAmount { get; set; }
        [NotMapped]
        public string CurrencyCode { get; set; }

    }
}
