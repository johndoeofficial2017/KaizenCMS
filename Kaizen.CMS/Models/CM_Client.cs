using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM_Client
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ShortName { get; set; }
        public string ClientDescription { get; set; }
        public string ContactPerson { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<int> AddressCode { get; set; }
        public Nullable<int> BillAddressCode { get; set; }
        public Nullable<int> RemitToAddressCode { get; set; }
        public string StatusName { get; set; }
        public string CUSTCLASNAME { get; set; }
        public string PrefixValue { get; set; }
        public Nullable<short> Prefixlengh { get; set; }
        public Nullable<int> LastClientID { get; set; }
        public int StatusID { get; set; }
        public string CUSTCLAS { get; set; }
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
    }
}
