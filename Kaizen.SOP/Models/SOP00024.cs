using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00024
    {
        public SOP00024()
        {
            this.SOP10101 = new List<SOP10101>();
            this.SOP10201 = new List<SOP10201>();
            this.SOP10251 = new List<SOP10251>();
        }

        public string ShippingID { get; set; }
        public string ShippingName { get; set; }
        public Nullable<byte> shippingtype { get; set; }
        public string Carrier { get; set; }
        public string CarrierAccount { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber02 { get; set; }
        public virtual ICollection<SOP10101> SOP10101 { get; set; }
        public virtual ICollection<SOP10201> SOP10201 { get; set; }
        public virtual ICollection<SOP10251> SOP10251 { get; set; }
    }
}
