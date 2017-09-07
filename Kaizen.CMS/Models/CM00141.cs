using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00141
    {
        public int AddressCodeType { get; set; }
        public string LegalID { get; set; }
        public string AddressName { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public string WebSite { get; set; }
        public string Pnone01 { get; set; }
        public string Pnone02 { get; set; }
        public string Pnone03 { get; set; }
        public string Pnone04 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string MobileNo3 { get; set; }
        public string MobileNo4 { get; set; }
        public string POBox { get; set; }
        public string Other01 { get; set; }
        public string Other02 { get; set; }
        public string Other03 { get; set; }
        public string Other04 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
        public string Email03 { get; set; }
        public string Email04 { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string Ext3 { get; set; }
        public string Ext4 { get; set; }
        public virtual CM00034 CM00034 { get; set; }
        public virtual CM00140 CM00140 { get; set; }
    }
}
