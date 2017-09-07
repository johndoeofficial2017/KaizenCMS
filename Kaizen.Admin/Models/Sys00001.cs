using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class Sys00001
    {
        public string CompanyAddressCode { get; set; }
        public string CompanyAddressName { get; set; }
        public string AddressName { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
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
        public virtual Sys00013 Sys00013 { get; set; }
        public virtual Sys00014 Sys00014 { get; set; }
    }
}
