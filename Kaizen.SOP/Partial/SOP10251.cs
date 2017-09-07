using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.SOP
{
    public partial class SOP10251
    {
        [NotMapped]
        public GL00100 SalesAccount { get; set; }
        [NotMapped]
        public GL00100 MarkdownAccount { get; set; }
        [NotMapped]
        public GL00100 FreightAccount { get; set; }
        [NotMapped]
        public GL00100 TradeDiscountAccount { get; set; }
        [NotMapped]
        public GL00100 MiscellaneousAccount { get; set; }
        [NotMapped]
        public GL00100 TaxAccount { get; set; }
        [NotMapped]
        public ICollection<IV00013> IV00013 { get; set; }
        [NotMapped]
        public string LotNumber { get; set; }
    }
}
